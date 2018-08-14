using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsRetentionPolicy
    {
        [JsonProperty(PropertyName = "daysToKeep")]
        public int DaysToKeep { get; set; }

        [JsonProperty(PropertyName = "releasesToKeep")]
        public int ReleasesToKeep { get; set; }

        [JsonProperty(PropertyName = "retainBuild")]
        public bool RetainBuild { get; set; }
    }
}
