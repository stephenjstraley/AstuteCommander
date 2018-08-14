using Newtonsoft.Json;
using System.Collections.Generic;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsReleaseDefinition
    {
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public string Revision { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public VstsBy CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public string CreatedOn { get; set; }

        [JsonProperty(PropertyName = "modifiedBy")]
        public VstsBy ModifiedBy { get; set; }

        [JsonProperty(PropertyName = "modifiedOn")]
        public string ModifiedOn { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public Dictionary<string, VstsValue> Variables { get; set; }

        [JsonProperty(PropertyName = "variableGroups")]
        public List<int> VariableGroups { get; set; }

        [JsonProperty(PropertyName = "environments")]
        public List<VstsEnvironment> Environments { get; set; }

        [JsonProperty(PropertyName = "artifacts")]
        public List<VstsReleaseDefinitionArtifact> Artifacts { get; set; }

        [JsonProperty(PropertyName = "triggers")]
        public List<VstsTrigger> Triggers { get; set; }

        [JsonProperty(PropertyName = "releaseNameFormat")]
        public string ReleaseNameFormat { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<object> Tags { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public object Properties { get; set; }

        public VstsBuildDefinition Build { get; set; }

    }
}
