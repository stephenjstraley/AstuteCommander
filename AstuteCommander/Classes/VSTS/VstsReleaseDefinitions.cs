using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsReleaseDefinitions : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsReleaseDefinition> ReleaseDefinitions { get; set; }
    }
}
