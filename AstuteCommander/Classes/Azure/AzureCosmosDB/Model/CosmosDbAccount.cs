using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmoDbAccount
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public object Tags { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public CosmosDbAccountProperty Properties { get; set; }

        public CosmosDbKeys Keys { get; set; }

        public string ResourceGroup { get; set; }

    }
}
