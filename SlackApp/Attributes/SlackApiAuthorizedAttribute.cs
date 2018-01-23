using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.Models;
using SlackApp.Repositories;

namespace SlackApp.Attributes
{
    public class SlackApiAuthorizedAttribute : TypeFilterAttribute
    {
        public SlackApiAuthorizedAttribute() : base(typeof(SlackApiAuthorizedFilter))
        {
        }

        private class SlackApiAuthorizedFilter : IActionFilter
        {
            private readonly TestAppConfig _testAppConfig;
            private readonly SlackWebApiConfig _slackWebApiConfig;
            private readonly IAppInstallRepository _appInstallRepo;

            public SlackApiAuthorizedFilter(IOptions<TestAppConfig> testAppOptions,
                IOptions<SlackWebApiConfig> slackWebApiOptions,
                IAppInstallRepository appInstallRepo)
            {
                _testAppConfig = testAppOptions.Value;
                _slackWebApiConfig = slackWebApiOptions.Value;
                _appInstallRepo = appInstallRepo;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                // get the slash command property
                var slashCommandProperty = context.ActionDescriptor.Parameters
                    .Single(p => p.ParameterType == typeof(SlashCommand));

                // get the slash command
                if (!context.ActionArguments.TryGetValue(slashCommandProperty.Name, out var argValue))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                var slashCommand = argValue as SlashCommand;

                // get the install
                var install = _appInstallRepo.GetAppInstall(slashCommand?.UserId);

                // set the result if an install doesn't exist
                if (install == null)
                {
                    context.Result = new OkObjectResult($"Add to slack -> {_slackWebApiConfig.AuthorizeUrl}?client_id={_testAppConfig.ClientId}&scope={_testAppConfig.Scope}");
                    return;
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
