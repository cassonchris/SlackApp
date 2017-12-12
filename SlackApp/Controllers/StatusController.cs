using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.Models;

namespace SlackApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Status")]
    public class StatusController : Controller
    {
        private readonly TestAppConfig _config;

        public StatusController(IOptions<TestAppConfig> options)
        {
            _config = options.Value;
        }

        [HttpPost]
        public IActionResult Post([FromForm] SlashCommand slashCommand)
        {
            return Redirect($"https://slack.com/oauth/authorize?client_id={_config.ClientId}&scope=dnd:write");
        }
    }
}