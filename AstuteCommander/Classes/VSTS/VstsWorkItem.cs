using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsWorkItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "rev")]
        public string Rev { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public VstsField Fields { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

    }
}
