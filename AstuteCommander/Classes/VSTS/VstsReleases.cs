using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsReleases : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsRelease> Release { get; set; }
    }
}
