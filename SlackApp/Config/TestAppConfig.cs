﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackApp.Config
{
    public class TestAppConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string VerificationToken { get; set; }
        public string RedirectUri { get; set; }
    }
}
