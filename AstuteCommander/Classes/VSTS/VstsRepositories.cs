using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsRepositories : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsRepository> Repositories { get; set; }
    }
}
