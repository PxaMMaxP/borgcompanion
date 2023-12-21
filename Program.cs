using borgcompanion.Classes;
using CommandLine;
using CommandLine.Text;

namespace borgcompanion;

class Program
{
    private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    static int Main(string[] args)
    {
        ActionOptions? options = null;

        // Parsing arguments
        var argumentsParser = new CommandLine.Parser(with => with.HelpWriter = null);
        var parserResult = argumentsParser.ParseArguments<PruneOptions, BackupOptions, CheckOptions>(args);

        // Handhabung der Parse-Ergebnisse
        parserResult
            .WithParsed<PruneOptions>(opts =>
            {
                logger.Trace($"Prune verb selected.");
                opts.ActionType = ActionType.Prune;
                options = opts;
            })
            .WithParsed<BackupOptions>(opts =>
            {
                logger.Trace($"Backup verb selected.");
                opts.ActionType = ActionType.Backup;
                options = opts;
            })
            .WithParsed<CheckOptions>(opts =>
            {
                logger.Trace($"Check verb selected.");
                opts.ActionType = ActionType.Check;
                options = opts;
            })
            .WithNotParsed(errors =>
            {
                if (errors.IsVersion() || errors.IsHelp())
                    logger.Trace($"Show help/version.");
                else
                    logger.Error($"Error while parsing: {errors}");

                var helpText = HelpText.AutoBuild(parserResult, h =>
                {
                    h.AddPreOptionsLine(AppConfig.ApplicationDescription);
                    return h;
                });
                Console.WriteLine(helpText);
            });

        if (options == null)
            return 0;
        else
            logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(options));

        // Load settings
        SettingsManager settingsManager = new SettingsManager(options?.GeneralConfigFile);
        logger.Debug(Newtonsoft.Json.JsonConvert.SerializeObject(settingsManager.Settings));

        // Configure logging
        Classes.Logger.ConfigureLogging(settingsManager.Settings.General.Logging);

        logger.Error($"Starting ...");
        logger.Trace($"... Starting");

        return 0;
    }
}
