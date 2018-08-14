using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildRetentionRule
    {
        [JsonProperty(PropertyName = "branches")]
        public List<string> Branches { get; set; }

        [JsonProperty(PropertyName = "artifacts")]
        public List<string> Artifacts { get; set; }

        [JsonProperty(PropertyName = "artifactTypesToDelete")]
        public List<string> ArtifactTypesToDelete { get; set; }

        [JsonProperty(PropertyName = "daysToKeep")]
        public int DaysToKeep { get; set; }

        [JsonProperty(PropertyName = "minimumToKeep")]
        public int MinimumToKeep { get; set; }

        [JsonProperty(PropertyName = "deleteBuildRecord")]
        public bool DeleteBuildRecord { get; set; }

        [JsonProperty(PropertyName = "deleteTestResults")]
        public bool DeleteTestResults { get; set; }

    }
}
