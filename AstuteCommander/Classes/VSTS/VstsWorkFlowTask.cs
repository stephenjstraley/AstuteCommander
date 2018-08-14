using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsWorkflowTask
    {
        [JsonProperty(PropertyName = "taskId")]
        public string TaskId { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "refName")]
        public string RefName { get; set; }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }

        [JsonProperty(PropertyName = "alwaysRun")]
        public bool AlwaysRun { get; set; }

        [JsonProperty(PropertyName = "continueOnError")]
        public bool ContinueOnError { get; set; }

        [JsonProperty(PropertyName = "timeoutInMinutes")]
        public int TimeoutInMinutes { get; set; }

        [JsonProperty(PropertyName = "definitionType")]
        public string DefinitionType { get; set; }

        [JsonProperty(PropertyName = "overrideInputs")]
        public object OverrideInputs { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public string Condition { get; set; }

        [JsonProperty(PropertyName = "inputs")]
        public VstsWorkflowInput Inputs { get; set; }
    }
}
