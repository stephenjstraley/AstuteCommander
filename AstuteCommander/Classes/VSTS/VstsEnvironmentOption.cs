using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsEnvironmentOption
    {
        [JsonProperty(PropertyName = "emailNotificationType")]
        public string EmailNotificationType { get; set; }

        [JsonProperty(PropertyName = "emailRecipients")]
        public string EmailRecipients { get; set; }

        [JsonProperty(PropertyName = "skipArtifactsDownload")]
        public bool SkipArtifactsDownload { get; set; }

        [JsonProperty(PropertyName = "timeoutInMinutes")]
        public int TimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "enableAccessToken")]
        public bool EnableAccessToken { get; set; }

        [JsonProperty(PropertyName = "publishDeploymentStatus")]
        public bool PublishDeploymentStatus { get; set; }
    }
}
