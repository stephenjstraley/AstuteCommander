using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsBuildDefinitions : BaseAPI
    {
        [JsonProperty(PropertyName = "value")]
        public List<VstsBuildDefinition> BuildDefinitions { get; set; }
    }
}
