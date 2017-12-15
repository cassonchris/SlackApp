using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SlackApp.Config;
using SlackApp.Models.SlackWebApi;
using SlackApp.Repositories;

namespace SlackApp.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAppInstallRepository _installRepo;
        private readonly TestAppConfig _testAppConfig;
        private readonly SlackWebApiConfig _slackWebApiConfig;
        private readonly HttpClient _httpClient;

        public AuthorizationService(IAppInstallRepository installRepo,
            IOptions<TestAppConfig> testAppOptions, 
            IOptions<SlackWebApiConfig> slackWebApiOptions)
        {
            _installRepo = installRepo;
            _testAppConfig = testAppOptions.Value;
            _slackWebApiConfig = slackWebApiOptions.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_slackWebApiConfig.BaseAddress)
            };
        }

        public async Task<bool> GrantAsync(string code)
        {
            var response = await _httpClient.GetAsync(
                $"{_slackWebApiConfig.OAuth.Access}?client_id={_testAppConfig.ClientId}&client_secret={_testAppConfig.ClientSecret}&code={code}&redirect_uri={_testAppConfig.RedirectUri}");

            var content = await response.Content.ReadAsStringAsync();

            var appInstall = JsonConvert.DeserializeObject<AppInstall>(content);

            if (appInstall.Ok)
            {
                var recordsSaved = await _installRepo.SaveAppInstallAsync(appInstall);

                if (recordsSaved > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
