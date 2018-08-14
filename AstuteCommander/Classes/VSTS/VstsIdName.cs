using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsIdName
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
