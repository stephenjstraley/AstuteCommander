using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildDefinition : VstsBuildDefinitionBase
    {
        [JsonProperty(PropertyName = "options")]
        public List<VstsBuildDefinitionOption> Options { get; set; }

        [JsonProperty(PropertyName = "triggers")]
        public List<VstsBuildTrigger> Triggers { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public VstsBuildVariable Variables { get; set; }

        [JsonProperty(PropertyName = "retentionRules")]
        public List<VstsBuildRetentionRule> RetentionRules { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        [JsonProperty(PropertyName = "buildNumberFormat")]
        public string BuildNumberFormat { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "jobAuthorizationScope")]
        public string JobAuthorizationScope { get; set; }

        [JsonProperty(PropertyName = "jobTimeoutInMinutes")]
        public string JobTimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "jobCancelTimeoutInMinutes")]
        public string JobCancelTimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "process")]
        public VstsBuildProcess Process { get; set; }

        [JsonProperty(PropertyName = "repository")]
        public VstsBuildRepository Repository { get; set; }

        [JsonProperty(PropertyName = "processParameters")]
        public object ProcessParameters { get; set; }

        [JsonProperty(PropertyName = "quality")]
        public string Quality { get; set; }

        [JsonProperty(PropertyName = "authoredBy")]
        public VstsBy AuthoredBy { get; set; }

        [JsonProperty(PropertyName = "queue")]
        public VstsBuildQueue Queue { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public string CreatedDate { get; set; }

    }
}
