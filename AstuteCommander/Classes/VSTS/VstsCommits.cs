using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsCommits : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsCommit> Commits { get; set; }

    }
}
