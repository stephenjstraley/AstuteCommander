using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsRepository
    {
        [JsonProperty(PropertyName = "id")]
        public string Guid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "project")]
        public VstsProject Project { get; set; }

        [JsonProperty(PropertyName = "defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "remoteUrl")]
        public string RemoteUrl { get; set; }

        [JsonProperty(PropertyName = "sshUrl")]
        public string SshUrl { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links{ get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "clean")]
        public string Clean { get; set; }

        [JsonProperty(PropertyName = "checkoutSubmodules")]
        public bool CheckoutSubmodules { get; set; }

    }
}
