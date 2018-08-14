using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountDbList
    {
        [JsonProperty(PropertyName = "_rid")]
        public string _Rid { get; set; }

        [JsonProperty(PropertyName = "Databases")]
        public List<CosmosDbAccountDb> Items { get; set; }

        [JsonProperty(PropertyName = "_count")]
        public int _Count { get; set; }

    }
}
