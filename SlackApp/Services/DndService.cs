using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SlackApp.Config;
using SlackApp.Models.SlackWebApi;

namespace SlackApp.Services
{
    public class DndService : IDndService
    {
        private readonly HttpClient _httpClient;
        private readonly SlackWebApiConfig _slackWebApiConfig;

        public DndService(IOptions<SlackWebApiConfig> slackWebApiOptions)
        {
            _slackWebApiConfig = slackWebApiOptions.Value;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_slackWebApiConfig.BaseAddress)
            };
        }

        public async Task<bool> SetSnooze(int duration, string accessToken)
        {
            var requestContent = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("num_minutes", duration.ToString()),
                new KeyValuePair<string, string>("token", accessToken)
            };

            var response = await _httpClient.PostAsync(_slackWebApiConfig.Dnd.SetSnooze, new FormUrlEncodedContent(requestContent));

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BaseResponse>(responseContent).Ok;
        }
    }
}
