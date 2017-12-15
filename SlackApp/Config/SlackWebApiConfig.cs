namespace SlackApp.Config
{
    public class SlackWebApiConfig
    {
        public string BaseAddress { get; set; }
        public Dnd Dnd { get; set; }
        public OAuth OAuth { get; set; }
    }

    public class Dnd
    {
        public string SetSnooze { get; set; }
    }

    public class OAuth
    {
        public string Access { get; set; }
    }
}
