using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsRelease
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "createdOn")]
        public string CreatedOn { get; set; }

        [JsonProperty(PropertyName = "modifiedOn")]
        public string ModifiedOn { get; set; }

        [JsonProperty(PropertyName = "modifiedBy")]
        public VstsBy ModifiedBy { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public VstsBy CreatedBy { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public object Variables { get; set; }

        [JsonProperty(PropertyName = "variableGroups")]
        public List<object> VariableGroups { get; set; }

        [JsonProperty(PropertyName = "releaseDefinition")]
        public VstsReleaseDefinition ReleaseDefinition { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "releaseNameFormat")]
        public string ReleaseNameFormat { get; set; }

        [JsonProperty(PropertyName = "keepForever")]
        public bool KeepForever { get; set; }

        [JsonProperty(PropertyName = "definitionSnapshotRevision")]
        public int DefinitionSnapshotRevision { get; set; }

        [JsonProperty(PropertyName = "logsContainerUrl")]
        public string LogsContainerUrl { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<object> Tags { get; set; }

        [JsonProperty(PropertyName = "projectReference")]
        public VstsIdName ProjectReference { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public object Properties { get; set; }

    }
}
