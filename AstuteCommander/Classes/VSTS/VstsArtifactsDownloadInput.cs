using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsArtifactsDownloadInput
    {
        [JsonProperty(PropertyName = "downloadInputs")]
        public List<object> DownloadInputs { get; set; }
    }
}
