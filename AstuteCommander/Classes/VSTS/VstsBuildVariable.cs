using Newtonsoft.Json;
namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildVariable
    {
        [JsonProperty(PropertyName = "system.debug")]
        public VstsVariableConfig Debug { get; set; }

        [JsonProperty(PropertyName = "BuildConfiguration")]
        public VstsVariableConfig BuildConfiguration { get; set; }

        [JsonProperty(PropertyName = "BuildPlatform")]
        public VstsVariableConfig BuildPlatform { get; set; }

    }
}
