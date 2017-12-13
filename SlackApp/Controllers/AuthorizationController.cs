using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApp.Config;

namespace SlackApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly TestAppConfig _config;
        private readonly HttpClient _client;

        public AuthorizationController(IOptions<TestAppConfig> options)
        {
            _config = options.Value;
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Grant(string code)
        {
            var uri =
                $"https://slack.com/api/oauth.access?client_id={_config.ClientId}&client_secret={_config.ClientSecret}&code={code}&redirect_uri={_config.RedirectUri}";

            var response = await _client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();

            return Ok(code);
        }
    }
}
