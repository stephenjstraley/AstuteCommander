using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsWorkItems : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsWorkItem> WorkItems { get; set; }

    }
}
