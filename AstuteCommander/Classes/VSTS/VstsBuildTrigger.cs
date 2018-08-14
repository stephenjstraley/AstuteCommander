using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildTrigger
    {
        [JsonProperty(PropertyName = "branchFilters")]
        public List<string> BranchFilters { get; set; }

        [JsonProperty(PropertyName = "pathFilters")]
        public List<string> PathFilters { get; set; }

        [JsonProperty(PropertyName = "batchChanges")]
        public string BatchChanges { get; set; }

        [JsonProperty(PropertyName = "maxConcurrentBuildsPerBranch")]
        public string MaxConcurrentBuildsPerBranch { get; set; }

        [JsonProperty(PropertyName = "triggerType")]
        public string TriggerType { get; set; }
    }
}
