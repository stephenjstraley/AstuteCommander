using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsReviewer
    {
        [JsonProperty(PropertyName = "reviewerUrl")]
        public string ReviewerUrl { get; set; }

        [JsonProperty(PropertyName = "vote")]
        public int Vote { get; set; }

        [JsonProperty(PropertyName = "isRequired")]
        public string IsRequired { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Guid { get; set; }

        [JsonProperty(PropertyName = "votedFor")]
        public List<VstsReviewer> VotedFor { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "uniqueName")]
        public string UniqueName { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "isContainer")]
        public string IsContainer { get; set; }

    }
}
