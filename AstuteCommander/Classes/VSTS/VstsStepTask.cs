using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsStepTask
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "versionSpec")]
        public string VersionSpec { get; set; }

        [JsonProperty(PropertyName = "definitionType")]
        public string DefinitionType { get; set; }

    }
}
