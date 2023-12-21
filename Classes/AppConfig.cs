using borgcompanion.Models.Settings;

namespace borgcompanion.Classes
{
    public static class AppConfig
    {
        public const string SettingsFilePath = "/etc/borgcompanion/settings.yaml";
        public static readonly LoggingSettings.LogLevel DefaultLogLevel = LoggingSettings.LogLevel.Info;

        public static readonly string ApplicationDescription = @"
        Hier ist eine kurze Beschreibung meines Programms.
        Diese Beschreibung kann mehrere Zeilen umfassen und wird im Hilfetext angezeigt.
        Weitere Informationen finden Sie unter: https://meinewebsite.com";
    }

}