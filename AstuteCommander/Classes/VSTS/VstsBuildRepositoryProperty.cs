using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildRepositoryProperty
    {
        [JsonProperty(PropertyName = "labelSources")]
        public string LabelSources { get; set; }

        [JsonProperty(PropertyName = "reportBuildStatus")]
        public string ReportBuildStatus { get; set; }

        [JsonProperty(PropertyName = "fetchDepth")]
        public string FetchDepth { get; set; }

        [JsonProperty(PropertyName = "gitLfsSupport")]
        public string GitLfsSupport { get; set; }

        [JsonProperty(PropertyName = "skipSyncSource")]
        public string SkipSyncSource { get; set; }

        [JsonProperty(PropertyName = "cleanOptions")]
        public string CleanOptions { get; set; }

        [JsonProperty(PropertyName = "checkoutNestedSubmodules")]
        public string CheckoutNestedSubmodules { get; set; }

        [JsonProperty(PropertyName = "labelSourcesFormat")]
        public string LabelSourcesFormat { get; set; }

    }
}
