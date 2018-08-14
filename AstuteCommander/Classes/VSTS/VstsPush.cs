using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPush
    {
        [JsonProperty(PropertyName = "pushId")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "repository")]
        public VstsRepository Repository { get; set; }

        [JsonProperty(PropertyName = "pushedBy")]
        public VstsBy PushedBy { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

    }
}
