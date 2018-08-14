using Newtonsoft.Json;
namespace AstuteCommander.Classes.VSTS
{
    public class VstsInput
    {
        [JsonProperty(PropertyName = "workItemType")]
        public string WorkItemType { get; set; }

        [JsonProperty(PropertyName = "assignToRequestor")]
        public string AssignToRequestor { get; set; }

        [JsonProperty(PropertyName = "additionalFields")]
        public string AdditionalFields { get; set; }

    }
}
