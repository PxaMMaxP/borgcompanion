using YamlDotNet.Serialization;

namespace borgcompanion.Models.Settings
{
    public class LoggingSettings
    {
        [YamlMember(Alias = "level")]
        public LogLevel Level { get; set; } = LogLevel.Info;

        [YamlMember(Alias = "file")]
        public string File { get; set; } = string.Empty;

        [YamlMember(Alias = "systemd")]
        public string Systemd { get; set; } = string.Empty;

        [YamlMember(Alias = "console")]
        public bool Console { get; set; } = true;

        public enum LogLevel
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }
    }
}