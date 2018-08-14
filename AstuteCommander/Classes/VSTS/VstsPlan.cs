using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPlan
    {
        [JsonProperty(PropertyName = "planId")]
        public string PlanId { get; set; }
    }
}
