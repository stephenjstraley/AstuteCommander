using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsStatusContext
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

    }
}
