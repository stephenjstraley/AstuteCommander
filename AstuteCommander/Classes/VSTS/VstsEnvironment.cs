using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsEnvironment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "Owner")]
        public VstsBy Owner { get; set; }

        [JsonProperty(PropertyName = "variables")]
        public Dictionary<string, VstsValue> Variables { get; set; }

        [JsonProperty(PropertyName = "preDeploymentApprovals")]
        public VstsApprovals PreDeploymentApprovals { get; set; }

        [JsonProperty(PropertyName = "deployStep")]
        public VstsDeployStep DeployStep { get; set; }

        [JsonProperty(PropertyName = "postDeployApprovals")]
        public VstsApprovals PostDeployApprovals { get; set; }

        [JsonProperty(PropertyName = "deployPhases")]
        public List<VstsDeployPhase> DeployPhases { get; set; }

        [JsonProperty(PropertyName = "environmentOptions")]
        public VstsEnvironmentOption EnvironmentOptions { get; set; }

        [JsonProperty(PropertyName = "demands")]
        public List<object> Demands { get; set; }

        [JsonProperty(PropertyName = "conditions")]
        public List<VstsCondition> Conditions { get; set; }

        [JsonProperty(PropertyName = "executionPolicy")]
        public VstsExecutionPolicy ExecutionPolicy { get; set; }

        [JsonProperty(PropertyName = "schedules")]
        public List<object> Schedules { get; set; }

        [JsonProperty(PropertyName = "retentionPolicy")]
        public VstsRetentionPolicy RetentionPolicy { get; set; }

        [JsonProperty(PropertyName = "processParameters")]
        public object ProcessParameters { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public object Properties { get; set; }
    }
}
