using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBranches : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsBranch> Branches { get; set; }

    }
}
