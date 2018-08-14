using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountCollection
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "indexingPolicy")]
        public CosmosDbAccountCollectionIndexingPolicy IndexingPolicy { get; set; }

        [JsonProperty(PropertyName = "_rid")]
        public string _Rid { get; set; }

        [JsonProperty(PropertyName = "_self")]
        public string _Self { get; set; }

        [JsonProperty(PropertyName = "_etag")]
        public string _ETag { get; set; }

        [JsonProperty(PropertyName = "_ts")]
        public string _Ts { get; set; }

        [JsonProperty(PropertyName = "_docs")]
        public string _Docs { get; set; }

        [JsonProperty(PropertyName = "_sprocs")]
        public string _Sprocs { get; set; }

        [JsonProperty(PropertyName = "_triggers")]
        public string _Triggers { get; set; }

        [JsonProperty(PropertyName = "_udfs")]
        public string _Udfs { get; set; }

        [JsonProperty(PropertyName = "_conflicts")]
        public string _Conflicts { get; set; }


    }
}
