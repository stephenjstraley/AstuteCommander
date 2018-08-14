using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsVariableConfig
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "allowOverride")]
        public bool AllowOverride { get; set; }
    }
}
