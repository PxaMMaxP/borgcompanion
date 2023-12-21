using borgcompanion.Models.Settings;
using NLog;
using NLog.Config;

namespace borgcompanion.Classes
{
    public static class Logger
    {
        public static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public static void ConfigureLogging(LoggingSettings? loggingSettings)
        {
            if (loggingSettings == null)
            {
                logger.Debug($"Logging settings have not been loaded.");
                return;
            }

            var config = LogManager.Configuration;
            if (config == null)
            {
                logger.Error($"Could not get NLog configuration.");
                throw new System.InvalidOperationException($"Could not get NLog configuration.");
            }

            // File target
            if (!string.IsNullOrWhiteSpace(loggingSettings.File))
            {
                var fileTarget = config.FindTargetByName<NLog.Targets.FileTarget>("logfile");
                if (fileTarget != null)
                {
                    fileTarget.FileName = loggingSettings.File;
                    fileTarget.ArchiveFileName = loggingSettings.File + ".{#}";

                    var fileRule = new LoggingRule("file");
                    fileRule.LoggerNamePattern = "*";
                    fileRule.SetLoggingLevels(GetNLogLevel(loggingSettings.Level), LogLevel.Fatal);
                    fileRule.Targets.Add(fileTarget);
                    config.LoggingRules.Add(fileRule);
                }
            }

            // Console target
            if (!loggingSettings.Console)
            {
                var consoleTarget = config.FindTargetByName<NLog.Targets.ConsoleTarget>("console");
                if (consoleTarget != null)
                    config.RemoveTarget("console");

                var consoleRule = config.FindRuleByName("console");
                if (consoleRule != null)
                    config.LoggingRules.Remove(consoleRule);
            }
            else
            {
                var consoleRule = config.FindRuleByName("console");
                if (consoleRule != null)
                    consoleRule.SetLoggingLevels(GetNLogLevel(loggingSettings.Level), LogLevel.Fatal);
            }

            // Systemd target
            if (!string.IsNullOrWhiteSpace(loggingSettings.Systemd))
            {
                var journaldTarget = config.FindTargetByName<NLog.Targets.Journald.JournaldTarget>("journald");
                if (journaldTarget != null)
                {
                    journaldTarget.SysLogIdentifier = loggingSettings.Systemd;

                    var journaldRule = new LoggingRule("journald");
                    journaldRule.LoggerNamePattern = "*";
                    journaldRule.SetLoggingLevels(GetNLogLevel(loggingSettings.Level), LogLevel.Fatal);
                    journaldRule.Targets.Add(journaldTarget);
                    config.LoggingRules.Add(journaldRule);
                }
            }

            LogManager.Configuration = config;
        }

        private static LogLevel GetNLogLevel(LoggingSettings.LogLevel Level)
        {
            return Level switch
            {
                LoggingSettings.LogLevel.Trace => LogLevel.Trace,
                LoggingSettings.LogLevel.Debug => LogLevel.Debug,
                LoggingSettings.LogLevel.Info => LogLevel.Info,
                LoggingSettings.LogLevel.Warn => LogLevel.Warn,
                LoggingSettings.LogLevel.Error => LogLevel.Error,
                LoggingSettings.LogLevel.Fatal => LogLevel.Fatal,
                _ => GetNLogLevel(AppConfig.DefaultLogLevel),
            };
        }

    }
}