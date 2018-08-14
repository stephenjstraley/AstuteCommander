using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsDeployPhase
    {
        [JsonProperty(PropertyName = "deploymentInput")]
        public VstsDeploymentInput DeploymentInput { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "phaseType")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "workflowTasks")]
        public List<VstsWorkflowTask> WorkflowTasks { get; set; }
    }
}
