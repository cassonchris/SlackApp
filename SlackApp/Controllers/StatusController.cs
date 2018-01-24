using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlackApp.Attributes;
using SlackApp.Models;
using SlackApp.Repositories;
using SlackApp.Services;

namespace SlackApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        private readonly IAppInstallRepository _appInstallRepo;
        private readonly IDndService _dndService;
        private readonly IUsersService _usersService;

        public StatusController(IAppInstallRepository appInstallRepo,
            IDndService dndService,
            IUsersService usersService)
        {
            _appInstallRepo = appInstallRepo;
            _dndService = dndService;
            _usersService = usersService;
        }

        [HttpPost]
        [SlackApiAuthorized]
        public async Task<IActionResult> Post([FromForm] SlashCommand slashCommand)
        {
            // todo - SlackApiAuthorized gets the install, how do we not run it twice?
            var install = _appInstallRepo.GetAppInstall(slashCommand.UserId);

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