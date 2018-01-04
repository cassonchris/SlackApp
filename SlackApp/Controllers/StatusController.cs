using System;
using System.Text;
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
        private readonly TestAppConfig _testAppConfig;
        private readonly SlackWebApiConfig _slackWebApiConfig;
        private readonly IAppInstallRepository _appInstallRepo;
        private readonly IDndService _dndService;
        private readonly IUsersService _usersService;

        public StatusController(IOptions<TestAppConfig> testAppOptions, 
            IOptions<SlackWebApiConfig> slackWebApiOptions,
            IAppInstallRepository appInstallRepo,
            IDndService dndService,
            IUsersService usersService)
        {
            _testAppConfig = testAppOptions.Value;
            _slackWebApiConfig = slackWebApiOptions.Value;
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
                return Ok($"Add to slack -> {_slackWebApiConfig.AuthorizeUrl}?client_id={_testAppConfig.ClientId}&scope={_testAppConfig.Scope}");
            }

            var commandParts = slashCommand.Text.Split(' ', 2);
            var durationString = commandParts[0];

            if (!int.TryParse(durationString, out var duration))
            {
                duration = 5;
            }

            var message = new StringBuilder();

            if (await _dndService.SetSnooze(duration, install.AccessToken))
            {
                message.AppendLine($"Snoozing for {duration} minutes.");
            }
            else
            {
                message.AppendLine("Failed to set snooze.");
            }

            if (commandParts.Length > 1)
            {
                var status = commandParts[1];
                if (!String.IsNullOrWhiteSpace(status))
                {
                    if (await _usersService.SetStatus(status, install.AccessToken))
                    {
                        message.AppendLine($"Status set to '{status}'");
                    }
                    else
                    {
                        message.AppendLine("Failed to set status.");
                    }
                }
            }

            return Ok(message.ToString());
        }
    }
}