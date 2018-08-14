using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsDefinitionReference
    {
        [JsonProperty(PropertyName = "artifactSourceDefinitionUrl")]
        public VstsIdName ArtifactSourceDefinitionUrl { get; set; }

        [JsonProperty(PropertyName = "defaultVersionBranch")]
        public VstsIdName DefaultVersionBranch { get; set; }

        [JsonProperty(PropertyName = "defaultVersionSpecific")]
        public VstsIdName DefaultVersionSpecific { get; set; }

        [JsonProperty(PropertyName = "defaultVersionTags")]
        public VstsIdName DefaultVersionTags { get; set; }

        [JsonProperty(PropertyName = "defaultVersionType")]
        public VstsIdName DefaultVersionType { get; set; }

        [JsonProperty(PropertyName = "definition")]
        public VstsIdName Definition { get; set; }

        [JsonProperty(PropertyName = "project")]
        public VstsIdName Project { get; set; }


    }
}
