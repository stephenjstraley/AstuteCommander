using Newtonsoft.Json;
namespace AstuteCommander.Classes.VSTS
{
    public class VstsOptionDefinition
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}
