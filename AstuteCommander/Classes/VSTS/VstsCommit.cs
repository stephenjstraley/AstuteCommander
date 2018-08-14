using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsCommit
    {
        [JsonProperty(PropertyName = "commitId")]
        public string Guid { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "remoteUrl")]
        public string RemoteUrl { get; set; }

        [JsonProperty(PropertyName = "author")]
        public VstsPerson Author { get; set; }

        [JsonProperty(PropertyName = "committer")]
        public VstsPerson Committer { get; set; }

        [JsonProperty(PropertyName = "changeCounts")]
        public VstsChange ChangeCounts { get; set; }

    }
}
