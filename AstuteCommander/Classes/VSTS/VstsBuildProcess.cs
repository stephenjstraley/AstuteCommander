using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildProcess
    {
        [JsonProperty(PropertyName = "phases")]
        public List<VstsPhaseProcess> Phases { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
