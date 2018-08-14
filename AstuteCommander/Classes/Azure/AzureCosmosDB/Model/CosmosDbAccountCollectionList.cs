using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountCollectionList
    {
        [JsonProperty(PropertyName = "_rid")]
        public string _Rid { get; set; }

        [JsonProperty(PropertyName = "DocumentCollections")]
        public List<CosmosDbAccountCollection> Items { get; set; }

        [JsonProperty(PropertyName = "_count")]
        public int _Count { get; set; }
    }
}
