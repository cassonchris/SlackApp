using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SlackApp.Config;
using SlackApp.Models.SlackWebApi;
using SlackApp.Repositories;

namespace SlackApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly TestAppConfig _config;
        private readonly IAppInstallRepository _installRepo;
        private readonly HttpClient _client;

        public AuthorizationController(IOptions<TestAppConfig> options, IAppInstallRepository installRepo)
        {
            _config = options.Value;
            _installRepo = installRepo;
            _client = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Grant(string code)
        {
            var uri =
                $"https://slack.com/api/oauth.access?client_id={_config.ClientId}&client_secret={_config.ClientSecret}&code={code}&redirect_uri={_config.RedirectUri}";

            var response = await _client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();

            var appInstall = JsonConvert.DeserializeObject<AppInstall>(content);

            if (appInstall.Ok)
            {
                var recordsSaved = await _installRepo.SaveAppInstallAsync(appInstall);

                if (recordsSaved > 0)
                {
                    return Ok(appInstall.AccessToken);
                }
                else
                {
                    return StatusCode(500, appInstall);
                }
            }

            return RedirectToAction("Index", "Install");
        }
    }
}
