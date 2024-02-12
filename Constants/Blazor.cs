namespace UltimateRemote.Constants;

internal static class Blazor
{
    public static class RouteTemplates
    {
        public const string Home = "/";
        public const string DeviceManager = $"/{nameof(DeviceManager)}";
        public const string StorageManager = $"/{nameof(StorageManager)}";
        public const string ConfigurationManager = $"/{nameof(ConfigurationManager)}";
        public const string LayoutManager = $"/{nameof(LayoutManager)}";
        public const string Preferences = $"/{nameof(Preferences)}";
        public const string BasicEditor = $"/{nameof(BasicEditor)}";
        public const string JukeboxManager = $"/{nameof(JukeboxManager)}";
        public const string Help = $"/{nameof(Help)}";
        public const string HelpSection = $"/{nameof(Help)}/{{Section:int}}";
        public const string Temp = $"/{nameof(Temp)}";
    }

    public static class Routes
    {
        public static string HelpSection(HelpTopicIdentifier topicIdentifier) =>
            $"/{nameof(RouteTemplates.Help)}/{(int)topicIdentifier}";
    }

}


