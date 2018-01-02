using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.ViewModels;

namespace SlackApp.Controllers
{
    public class InstallController : Controller
    {
        private readonly TestAppConfig _testAppConfig;
        private readonly SlackWebApiConfig _slackWebApiConfig;

        public InstallController(IOptions<TestAppConfig> testAppOptions, IOptions<SlackWebApiConfig> slackWebApiOptions)
        {
            _testAppConfig = testAppOptions.Value;
            _slackWebApiConfig = slackWebApiOptions.Value;
        }

        public IActionResult Index()
        {
            var vm = new InstallViewModel
            {
                AuthorizeUrl =
                    $"{_slackWebApiConfig.AuthorizeUrl}?client_id={_testAppConfig.ClientId}&scope={_testAppConfig.Scope}"
            };

            return View(vm);
        }
    }
}
