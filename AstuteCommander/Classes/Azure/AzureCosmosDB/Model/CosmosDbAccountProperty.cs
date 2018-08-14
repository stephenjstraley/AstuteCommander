using System.Collections.Generic;
using Newtonsoft.Json;
using AstuteCommander.Classes.Azure.Model;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountProperty
    {
        [JsonProperty(PropertyName = "provisioningState")]
        public string ProvisioningState { get; set; }

        [JsonProperty(PropertyName = "documentEndpoint")]
        public string DocumentEndpoint { get; set; }

        [JsonProperty(PropertyName = "ipRangeFilter")]
        public string IpRangeFilter { get; set; }

        [JsonProperty(PropertyName = "databaseAccountOfferType")]
        public string DatabaseAccountOfferType { get; set; }

        [JsonProperty(PropertyName = "consistencyPolicy")]
        public CosmosDbAccountPropertyConsistency ConsistencyPolicy { get; set; }

        [JsonProperty(PropertyName = "writeLocations")]
        public List<AzureLocations> WriteLocations { get; set; }

        [JsonProperty(PropertyName = "readLocations")]
        public List<AzureLocations> ReadLocations { get; set; }

        [JsonProperty(PropertyName = "failoverPolicies")]
        public List<AzurePolicies> FailoverPolicies { get; set; }

    }
}
