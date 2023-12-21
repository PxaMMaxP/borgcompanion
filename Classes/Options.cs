using borgcompanion.Models.Settings;
using CommandLine;

namespace borgcompanion.Classes
{
    public class CommonOptions
    {
        public ActionType ActionType { get; set; } = ActionType.None;

        [Option(
            'l',
            "log-level",
            Default = null,
            Required = false,
            HelpText = "Set the log level.\n" +
                        "Possible values: Trace, Debug, Info, Warn, Error, Fatal.")]
        public LoggingSettings.LogLevel? LogLevel { get; set; }

        [Option(
            "test",
            Default = false,
            HelpText = "Test mode activated. No actual Borg commands are executed in this mode.")]
        public bool TestMode { get; set; }

        [Option(
            'g',
            "general-config-file",
            Default = null,
            Required = false,
            HelpText = "Specify the general settings file to use.")]
        public string? GeneralConfigFile { get; set; }
    }

    public abstract class ActionOptions : CommonOptions
    {
        [Option(
            'f',
            "config-file",
            Group = "working",
            Required = false,
            HelpText = "Specify a configuration file.")]
        public string ConfigFile { get; set; }

        [Option(
            'd',
            "config-dir",
            Group = "working",
            Required = false,
            HelpText = "Specify a directory of configuration files.")]
        public string ConfigDir { get; set; }
    }

    [Verb("prune", HelpText = "Prune action.")]
    public class PruneOptions : ActionOptions
    { }

    [Verb("backup", HelpText = "Backup action.")]
    public class BackupOptions : ActionOptions
    { }

    [Verb("check", HelpText = "Check action.")]
    public class CheckOptions : ActionOptions
    { }

    public enum ActionType
    {
        Prune,
        Backup,
        Check,
        None  // Standardwert, falls kein Verb angegeben wurde
    }
}
