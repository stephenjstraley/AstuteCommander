using System.Collections.Generic;
using Newtonsoft.Json;
using AstuteCommander.Classes.Azure.Model;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbAccountCollectionIndexingPolicy
    {
        [JsonProperty(PropertyName = "indexingMode")]
        public string IndexingMode { get; set; }

        [JsonProperty(PropertyName = "automatic")]
        public bool Automatic { get; set; }

        [JsonProperty(PropertyName = "includedPaths")]
        public List<AzurePath> IncludedPaths { get; set; }

        [JsonProperty(PropertyName = "excludedPaths")]
        public List<AzurePath> ExcludedPaths { get; set; }
    }
}
