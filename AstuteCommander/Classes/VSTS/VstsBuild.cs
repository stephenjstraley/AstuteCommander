using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuild
    {
        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        [JsonProperty(PropertyName = "plans")]
        public List<VstsPlan> Plans { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }

        [JsonProperty(PropertyName = "queueTime")]
        public string QueueTime { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        public string StartTime { get; set; }

        [JsonProperty(PropertyName = "finishTime")]
        public string FinishTime { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "definition")]
        public VstsBuildDefinitionBase Definition { get; set; }

        [JsonProperty(PropertyName = "project")]
        public VstsProject Project { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "sourceBranch")]
        public string SourceBranch { get; set; }

        [JsonProperty(PropertyName = "sourceVersion")]
        public string SourceVersion { get; set; }

        [JsonProperty(PropertyName = "queue")]
        public VstsBuildQueue Queue { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public string Priority { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "requestedFor")]
        public VstsBy RequestedFor { get; set; }

        [JsonProperty(PropertyName = "requestedBy")]
        public VstsBy RequestedBy { get; set; }

        [JsonProperty(PropertyName = "lastChangedDate")]
        public string LastChangedDate { get; set; }

        [JsonProperty(PropertyName = "lastChangedBy")]
        public VstsBy LastChangedBy { get; set; }

        [JsonProperty(PropertyName = "orchestrationPlan")]
        public VstsPlan OrchestrationPlan { get; set; }

        [JsonProperty(PropertyName = "logs")]
        public VstsBuildLog Logs { get; set; }

        [JsonProperty(PropertyName = "repository")]
        public VstsRepository Repository { get; set; }

        [JsonProperty(PropertyName = "keepForever")]
        public string KeepForever { get; set; }

        [JsonProperty(PropertyName = "retainedByRelease")]
        public string RetainedByRelease { get; set; }







    }
}
