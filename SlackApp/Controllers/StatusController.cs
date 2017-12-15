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

        public StatusController(IOptions<TestAppConfig> options, 
            IAppInstallRepository appInstallRepo,
            IDndService dndService)
        {
            _config = options.Value;
            _appInstallRepo = appInstallRepo;
            _dndService = dndService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SlashCommand slashCommand)
        {
            var install = _appInstallRepo.GetAppInstall(slashCommand.UserId);
            if (install == null)
            {
                return Redirect($"https://slack.com/oauth/authorize?client_id={_config.ClientId}&scope=dnd:write");
            }

            if (!int.TryParse(slashCommand.Text, out var duration))
            {
                duration = 5;
            }

            var ok = await _dndService.SetSnooze(duration, install.AccessToken);

            if (ok)
            {
                return Ok($"Snoozing for {duration} minutes");
            }
            else
            {
                return StatusCode(500, "Can't sleep... too much red bull!!");
            }
        }
    }
}