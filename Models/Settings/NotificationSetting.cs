using YamlDotNet.Serialization;

namespace borgcompanion.Models.Settings
{
    public class NotificationSettings
    {
        [YamlMember(Alias = "to_email")]
        public string ToEmail { get; set; }

        [YamlMember(Alias = "account")]
        public string Account { get; set; }

        [YamlMember(Alias = "info_day")]
        public string InfoDay { get; set; }

    }
}