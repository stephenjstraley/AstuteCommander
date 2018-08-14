using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsExecutionPolicy
    {
        [JsonProperty(PropertyName = "concurrencyCount")]
        public int ConcurrencyCount { get; set; }

        [JsonProperty(PropertyName = "queueDepthCount")]
        public int QueueDepthCount { get; set; }
    }
}
