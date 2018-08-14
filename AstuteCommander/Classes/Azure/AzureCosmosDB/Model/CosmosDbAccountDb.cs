using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountDb
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_rid")]
        public string _Rid { get; set; }

        [JsonProperty(PropertyName = "_self")]
        public string _Self { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string _ETag { get; set; }

        [JsonProperty(PropertyName = "_colls")]
        public string _Colls { get; set; }

        [JsonProperty(PropertyName = "_users")]
        public string _Users { get; set; }

        [JsonProperty(PropertyName = "_ts")]
        public string _Ts { get; set; }

    }
}
