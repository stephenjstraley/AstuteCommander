using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public static class CommonAPI
    {
        public class Href
        {
            [JsonProperty(PropertyName = "href")]
            public string HRef { get; set; }
        }

        public class By
        {
            [JsonProperty(PropertyName = "id")]
            public string Guid { get; set; }

            [JsonProperty(PropertyName = "displayName")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "uniqueName")]
            public string Email { get; set; }

            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }

            [JsonProperty(PropertyName = "imageUrl")]
            public string ImageUrl { get; set; }
        }

        public class Reviewer
        {
            [JsonProperty(PropertyName = "reviewerUrl")]
            public string ReviewerUrl { get; set; }

            [JsonProperty(PropertyName = "vote")]
            public string Vote { get; set; }

            [JsonProperty(PropertyName = "votedFor")]
            public List<Voting> VotedFor { get; set; }

            [JsonProperty(PropertyName = "id")]
            public string Guid { get; set; }

            [JsonProperty(PropertyName = "displayName")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "uniqueName")]
            public string Email { get; set; }

            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }

            [JsonProperty(PropertyName = "imageUrl")]
            public string ImageUrl { get; set; }
        }

        public class Voting
        {
            [JsonProperty(PropertyName = "reviewerUrl")]
            public string ReviewerUrl { get; set; }

            [JsonProperty(PropertyName = "vote")]
            public string Vote { get; set; }

            [JsonProperty(PropertyName = "id")]
            public string Guid { get; set; }

            [JsonProperty(PropertyName = "displayName")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "uniqueName")]
            public string UniqueName { get; set; }

            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }

            [JsonProperty(PropertyName = "imageUrl")]
            public string ImageUrl { get; set; }

            [JsonProperty(PropertyName = "isContainer")]
            public string IsContainer { get; set; }
        }

        public class Commit
        {
            [JsonProperty(PropertyName = "commitId")]
            public string Guid { get; set; }

            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }
        }

        public class Debug
        {
            [JsonProperty(PropertyName = "system.debug")]
            public Value Value { get; set; }
        }
        public class Value
        {
            [JsonProperty(PropertyName = "value")]
            public string Val { get; set; }
        }
    }
}
