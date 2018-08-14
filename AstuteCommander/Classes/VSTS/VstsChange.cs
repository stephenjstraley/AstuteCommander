using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsChange
    {
        [JsonProperty(PropertyName = "Edit")]
        public int Edit { get; set; }

        [JsonProperty(PropertyName = "Add")]
        public int Add { get; set; }

        [JsonProperty(PropertyName = "Delete")]
        public int Delete { get; set; }

    }
}
