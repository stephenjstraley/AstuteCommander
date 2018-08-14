using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.Azure.Model
{
    public class AzurePath
    {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "indexes")]
        public List<AzureIndex> Indexes { get; set; }
    }
}
