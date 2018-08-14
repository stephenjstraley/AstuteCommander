using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPullRequests : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsPullRequest> PullRequests { get; set; }
    }
}
