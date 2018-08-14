using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPerson
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
    }
}
