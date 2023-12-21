using YamlDotNet.Serialization;

namespace borgcompanion.Models.Settings
{
    public class GeneralSettings
    {
        [YamlMember(Alias = "notification")]
        public NotificationSettings? Notification { get; set; } = null;

        [YamlMember(Alias = "logging")]
        public LoggingSettings? Logging { get; set; } = null;
    }
}