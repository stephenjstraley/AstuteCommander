using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPushes : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsPush> Pushes { get; set; }

    }
}
