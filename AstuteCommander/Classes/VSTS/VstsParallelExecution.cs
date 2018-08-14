using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsParallelExecution
    {
        [JsonProperty(PropertyName = "multipliers")]
        public string Multipliers { get; set; }

        [JsonProperty(PropertyName = "maxNumberOfAgents")]
        public int MaxNumberOfAgents { get; set; }

        [JsonProperty(PropertyName = "continueOnError")]
        public bool ContinueOnError { get; set; }

        [JsonProperty(PropertyName = "parallelExecutionType")]
        public string ParallelExecutionType { get; set; }
    }
}
