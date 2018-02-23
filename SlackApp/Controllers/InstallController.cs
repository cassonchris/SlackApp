using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.ViewModels;

namespace SlackApp.Controllers
{
    public class InstallController : Controller
    {
        private readonly SlackAppConfig _slackAppConfig;
        private readonly SlackWebApiConfig _slackWebApiConfig;

        public InstallController(IOptions<SlackAppConfig> slackAppOptions, IOptions<SlackWebApiConfig> slackWebApiOptions)
        {
            _slackAppConfig = slackAppOptions.Value;
            _slackWebApiConfig = slackWebApiOptions.Value;
        }

        public IActionResult Index()
        {
            var vm = new InstallViewModel
            {
                AuthorizeUrl =
                    $"{_slackWebApiConfig.AuthorizeUrl}?client_id={_slackAppConfig.ClientId}&scope={_slackAppConfig.Scope}"
            };

            return View(vm);
        }
    }
}
