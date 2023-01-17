namespace ConfigurationAndLogging.Configurations
{
    public class GameSettings
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string[] Updates { get; set; } = default!;
    }
}
