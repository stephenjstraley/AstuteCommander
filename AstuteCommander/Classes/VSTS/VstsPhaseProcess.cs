using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPhaseProcess
    {
        [JsonProperty(PropertyName = "steps")]
        public List<VstsPhaseStep> Steps { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "jobAuthorizationScope")]
        public string JobAuthorizationScope { get; set; }

    }
}
