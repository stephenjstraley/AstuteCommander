using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildRepository
    {
        [JsonProperty(PropertyName = "properties")]
        public VstsBuildRepositoryProperty Properties { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "clean")]
        public string Clean { get; set; }

        [JsonProperty(PropertyName = "checkoutSubmodules")]
        public string CheckoutSubmodules { get; set; }

    }
}
