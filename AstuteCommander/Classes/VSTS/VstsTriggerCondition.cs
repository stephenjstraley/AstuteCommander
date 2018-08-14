using Newtonsoft.Json;
using System.Collections.Generic;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsTriggerCondition
    {
        [JsonProperty(PropertyName = "sourceBranch")]
        public string SourceBranch { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<object> Tags { get; set; }

    }
}
