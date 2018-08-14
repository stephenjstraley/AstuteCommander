using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.Model
{
    public class AzureLocations
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "locationName")]
        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "documentEndpoint")]
        public string DocumentEndpoint { get; set; }

        [JsonProperty(PropertyName = "provisioningState")]
        public string ProvisioningState { get; set; }

        [JsonProperty(PropertyName = "failoverPriority")]
        public int FailoverPriority { get; set; }

    }
}
