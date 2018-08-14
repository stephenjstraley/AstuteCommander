using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPhaseStep
    {
        [JsonProperty(PropertyName = "environment")]
        public object Environment { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "continueOnError")]
        public bool ContinueOnError { get; set; }

        [JsonProperty(PropertyName = "alwaysRun")]
        public bool AlwaysRun { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "timeoutInMinutes")]
        public int TimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public string Condition { get; set; }

        [JsonProperty(PropertyName = "refName")]
        public string RefName { get; set; }

        [JsonProperty(PropertyName = "quality")]
        public string Quality { get; set; }

        [JsonProperty(PropertyName = "task")]
        public VstsStepTask Task { get; set; }

        [JsonProperty(PropertyName = "inputs")]
        public VstsStepInput Inputs { get; set; }

    }
}
