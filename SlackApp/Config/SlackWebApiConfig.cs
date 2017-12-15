namespace SlackApp.Config
{
    public class SlackWebApiConfig
    {
        public string BaseAddress { get; set; }
        public Dnd Dnd { get; set; }
    }

    public class Dnd
    {
        public string SetSnooze { get; set; }
    }
}
