using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildLog
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

    }
}
