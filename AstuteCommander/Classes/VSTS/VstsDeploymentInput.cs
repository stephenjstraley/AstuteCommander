using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsDeploymentInput
    {
        [JsonProperty(PropertyName = "parallelExecution")]
        public VstsParallelExecution ParallelExecution { get; set; }

        [JsonProperty(PropertyName = "skipArtifactsDownload")]
        public bool SkipArtifactsDownload { get; set; }

        [JsonProperty(PropertyName = "artifactsDownloadInput")]
        public VstsArtifactsDownloadInput ArtifactsDownloadInput { get; set; }

        [JsonProperty(PropertyName = "queueId")]
        public string QueueId { get; set; }

        [JsonProperty(PropertyName = "demands")]
        public List<string> Demands { get; set; }

        [JsonProperty(PropertyName = "enableAccessToken")]
        public bool EnableAccessToken { get; set; }

        [JsonProperty(PropertyName = "timeoutInMinutes")]
        public int TimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "jobCancelTimeoutInMinutes")]
        public int JobCancelTimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public string Condition { get; set; }

        [JsonProperty(PropertyName = "overrideInputs")]
        public object OverrideInputs { get; set; }
    }
}
