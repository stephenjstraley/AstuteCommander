using Newtonsoft.Json;
namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildDefinitionOption
    {
        [JsonProperty(PropertyName = "enabled")]
        public string Enabled { get; set; }

        [JsonProperty(PropertyName = "definition")]
        public VstsOptionDefinition Definition { get; set; }

        [JsonProperty(PropertyName = "inputs")]
        public VstsInput Inputs { get; set; }

    }
}
