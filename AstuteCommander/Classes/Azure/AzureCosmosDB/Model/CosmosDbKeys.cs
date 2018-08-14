using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.AzureCosmosDB.Model
{
    public class CosmosDbKeys
    {
        [JsonProperty(PropertyName = "primaryMasterKey")]
        public string PrimaryMasterKey { get; set; }

        [JsonProperty(PropertyName = "secondaryMasterKey")]
        public string SecondaryMasterKey { get; set; }

        [JsonProperty(PropertyName = "primaryReadonlyMasterKey")]
        public string PrimaryReadonlyMasterKey { get; set; }

        [JsonProperty(PropertyName = "secondaryReadonlyMasterKey")]
        public string RecondaryReadonlyMasterKey { get; set; }
    }
}
