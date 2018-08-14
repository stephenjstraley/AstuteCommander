using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountList
    {
        [JsonProperty(PropertyName = "value")]
        public List<CosmoDbAccount> Items { get; set; }
    }
}
