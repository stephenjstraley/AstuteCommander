using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsApproval
    {
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "isAutomated")]
        public bool IsAutomated { get; set; }

        [JsonProperty(PropertyName = "isNotificationOn")]
        public bool IsNotificationOn { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
