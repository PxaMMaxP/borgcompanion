using borgcompanion.Models;
using YamlDotNet.Serialization;

namespace borgcompanion.Classes
{
    public class SettingsManager
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private SettingsModel? settings;

        public SettingsModel Settings
        {
            get
            {
                if (settings == null)
                {
                    logger.Error($"Settings have not been loaded.");
                    throw new System.InvalidOperationException($"Settings have not been loaded.");
                }
                else
                    return settings;
            }
        }

        public SettingsManager(string? filePath)
        {

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = AppConfig.SettingsFilePath;
            }

            settings = LoadSettingsFromFile(filePath);
        }

        private static string LoadSettingsFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error while reading settings file '{filePath}'");
                throw;
            }
        }

        private static SettingsModel? LoadSettingsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                logger.Error($"Settings file '{filePath}' does not exist.");
                throw new FileNotFoundException($"Settings file '{filePath}' does not exist.", filePath);
            }

            IDeserializer deserializer = new DeserializerBuilder().Build();
            string settingsContent = LoadSettingsFile(filePath);

            if (!String.IsNullOrWhiteSpace(settingsContent))
            {
                SettingsModel? settings = null;

                try
                {
                    settings = deserializer.Deserialize<SettingsModel>(settingsContent);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Error deserializing YAMLfrom file {filePath}.");
                    throw;
                }

                return settings;
            }
            else
            {
                logger.Error($"No YAML found in file {filePath}.");
                throw new InvalidOperationException($"No YAML found in file {filePath}.");
            }
        }
    }
}