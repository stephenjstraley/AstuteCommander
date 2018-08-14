using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.Model
{
    public class AzurePolicies
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "locationName")]
        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "failoverPriority")]
        public int FailoverPriority { get; set; }

    }
}
