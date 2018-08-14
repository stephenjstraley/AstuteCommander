using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsStatus
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "context")]
        public VstsStatusContext VstsStatusContext { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public VstsBy CreatedBy { get; set; }

        [JsonProperty(PropertyName = "targetUrl")]
        public string TargetUrl { get; set; }
    }
}
