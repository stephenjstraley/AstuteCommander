using Newtonsoft.Json;
using System.Collections.Generic;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsTrigger
    {
        [JsonProperty(PropertyName = "artifactAlias")]
        public string ArtifactAlias { get; set; }

        [JsonProperty(PropertyName = "triggerConditions")]
        public List<VstsTriggerCondition> TriggerConditions { get; set; }

        [JsonProperty(PropertyName = "triggerType")]
        public string TriggerType { get; set; }

    }
}
