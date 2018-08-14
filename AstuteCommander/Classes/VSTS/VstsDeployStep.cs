using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsDeployStep
    {
        [JsonProperty(PropertyName = "tasks")]
        public List<object> Tasks { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
