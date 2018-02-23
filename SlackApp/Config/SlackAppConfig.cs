namespace SlackApp.Config
{
    public class SlackAppConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string VerificationToken { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string SlackAppConnection { get; set; }
    }
}
