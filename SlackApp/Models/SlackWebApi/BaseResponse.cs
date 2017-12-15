using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SlackApp.Models.SlackWebApi
{
    public class BaseResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }
    }
}
