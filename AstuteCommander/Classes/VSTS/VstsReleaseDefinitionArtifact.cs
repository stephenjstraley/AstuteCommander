using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsReleaseDefinitionArtifact
    {
        [JsonProperty(PropertyName = "sourceId")]
        public string SourceId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "alias")]
        public string Alias { get; set; }

        [JsonProperty(PropertyName = "definitionReference")]
        public VstsDefinitionReference DefinitionReference { get; set; }

        [JsonProperty(PropertyName = "isPrimart")]
        public bool IsPrimary { get; set; }
    }
}
