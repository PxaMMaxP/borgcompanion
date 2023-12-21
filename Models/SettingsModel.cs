
using borgcompanion.Models.Settings;
using YamlDotNet.Serialization;

namespace borgcompanion.Models 
{
    public class SettingsModel
    {
        [YamlMember(Alias = "general")]
        public GeneralSettings General { get; set; }
    }
}