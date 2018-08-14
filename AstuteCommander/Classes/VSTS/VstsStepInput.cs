using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsStepInput
    {
        [JsonProperty(PropertyName = "solution")]
        public string Solution { get; set; }

        [JsonProperty(PropertyName = "nugetConfigPath")]
        public string NugetConfigPath { get; set; }

        [JsonProperty(PropertyName = "restoreMode")]
        public string RestoreMode { get; set; }

        [JsonProperty(PropertyName = "noCache")]
        public string NoCache { get; set; }

        [JsonProperty(PropertyName = "nuGetRestoreArgs")]
        public string NuGetRestoreArgs { get; set; }

        [JsonProperty(PropertyName = "verbosity")]
        public string Verbosity { get; set; }

        [JsonProperty(PropertyName = "nuGetVersion")]
        public string NuGetVersion { get; set; }

        [JsonProperty(PropertyName = "nuGetPath")]
        public string NuGetPath { get; set; }
    }
}
