using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB
{
    public class CosmosDbAccountPropertyConsistency
    {
        [JsonProperty(PropertyName = "defaultConsistencyLevel")]
        public string DefaultConsistencyLevel { get; set; }

        [JsonProperty(PropertyName = "maxIntervalInSeconds")]
        public int MaxIntervalInSeconds { get; set; }

        [JsonProperty(PropertyName = "maxStalenessPrefix")]
        public int MaxStalenessPrefix { get; set; }
    }
}
