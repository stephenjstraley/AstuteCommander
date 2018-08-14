using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBranch
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "objectId")]
        public string Guid { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "isLockedBy")]
        public VstsBy Locked { get; set; }

        [JsonProperty(PropertyName = "isLocked")]
        public string IsLocked { get; set; }

        [JsonProperty(PropertyName = "statuses")]
        public List<VstsStatus> Statuses { get; set; }

    }
}
