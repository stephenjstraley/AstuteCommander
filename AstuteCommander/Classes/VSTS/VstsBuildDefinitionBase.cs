using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildDefinitionBase
    {
        [JsonProperty(PropertyName = "drafts")]
        public List<object> Drafts { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "queueStatus")]
        public string QueueStatus { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public string Revision { get; set; }

        [JsonProperty(PropertyName = "project")]
        public VstsProject Project { get; set; }
    }
}
