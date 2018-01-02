using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.Models;
using SlackApp.Repositories;
using SlackApp.Services;

namespace SlackApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        private readonly TestAppConfig _config;
        private readonly IAppInstallRepository _appInstallRepo;
        private readonly IDndService _dndService;
        private readonly IUsersService _usersService;

        public StatusController(IOptions<TestAppConfig> options, 
            IAppInstallRepository appInstallRepo,
            IDndService dndService,
            IUsersService usersService)
        {
            _config = options.Value;
            _appInstallRepo = appInstallRepo;
            _dndService = dndService;
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SlashCommand slashCommand)
        {
            var install = _appInstallRepo.GetAppInstall(slashCommand.UserId);
            if (install == null)
            {
                return Redirect($"https://slack.com/oauth/authorize?client_id={_config.ClientId}&scope=dnd:write,users:write");
            }

            var commandParts = slashCommand.Text.Split(' ', 2);
            var durationString = commandParts[0];
            var presence = commandParts[1];

            if (!int.TryParse(durationString, out var duration))
            {
                duration = 5;
            }

            var dndOk = await _dndService.SetSnooze(duration, install.AccessToken);
            var presenceOk = await _usersService.SetPresence(presence, install.AccessToken);

            if (dndOk && presenceOk)
            {
                return Ok($"Snoozing for {duration} minutes. Presence set to {presence}.");
            }
            else
            {
                return StatusCode(500, "Can't sleep... too much red bull!!");
            }
        }
    }
}