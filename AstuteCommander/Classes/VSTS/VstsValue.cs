using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsValue
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
