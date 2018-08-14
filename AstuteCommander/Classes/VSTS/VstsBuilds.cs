using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuilds : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsBuild> Builds { get; set; }
    }
}
