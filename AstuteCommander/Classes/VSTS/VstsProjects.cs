using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsProjects : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsProject> Projects { get; set; }
    }
}
