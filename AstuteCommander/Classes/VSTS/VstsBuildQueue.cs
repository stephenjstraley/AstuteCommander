using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildQueue
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "pool")]
        public VstsIdName Pool { get; set; }

    }
}
