using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.Model
{
    public class AzureIndex
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "dataType")]
        public string DataType { get; set; }

        public int Precision { get; set; }

    }
}
