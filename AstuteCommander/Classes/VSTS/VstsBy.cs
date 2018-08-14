using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBy
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string By { get; set; }

        [JsonProperty(PropertyName = "uniqueName")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
