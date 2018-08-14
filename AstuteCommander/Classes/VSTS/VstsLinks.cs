using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsLinks
    {
        [JsonProperty(PropertyName = "self")]
        public CommonAPI.Href Self { get; set; }

        [JsonProperty(PropertyName = "project")]
        public CommonAPI.Href Project { get; set; }

        [JsonProperty(PropertyName = "repository")]
        public CommonAPI.Href Repository { get; set; }

        [JsonProperty(PropertyName = "workItems")]
        public CommonAPI.Href WorkItems { get; set; }

        [JsonProperty(PropertyName = "sourceBranch")]
        public CommonAPI.Href SourceBranch { get; set; }

        [JsonProperty(PropertyName = "targetBranch")]
        public CommonAPI.Href TargetBranch { get; set; }

        [JsonProperty(PropertyName = "statuses")]
        public CommonAPI.Href Statuses { get; set; }

        [JsonProperty(PropertyName = "sourceCommit")]
        public CommonAPI.Href SourceCommit { get; set; }

        [JsonProperty(PropertyName = "targetCommit")]
        public CommonAPI.Href TargetCommit { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public CommonAPI.Href CreatedBy { get; set; }

        [JsonProperty(PropertyName = "iterations")]
        public CommonAPI.Href Iterations { get; set; }

        [JsonProperty(PropertyName = "web")]
        public CommonAPI.Href Web { get; set; }

        [JsonProperty(PropertyName = "commits")]
        public CommonAPI.Href Commits { get; set; }

        [JsonProperty(PropertyName = "refs")]
        public CommonAPI.Href Refs { get; set; }

        [JsonProperty(PropertyName = "pullRequests")]
        public CommonAPI.Href PullRequests { get; set; }

        [JsonProperty(PropertyName = "items")]
        public CommonAPI.Href Branches { get; set; }

        [JsonProperty(PropertyName = "pushes")]
        public CommonAPI.Href Pushes { get; set; }

        [JsonProperty(PropertyName = "workItemUpdates")]
        public CommonAPI.Href WorkItemUpdates { get; set; }

        [JsonProperty(PropertyName = "workItemHistory")]
        public CommonAPI.Href WorkItemHistory { get; set; }

        [JsonProperty(PropertyName = "workItemRevisions")]
        public CommonAPI.Href WorkItemRevisions { get; set; }

        [JsonProperty(PropertyName = "html")]
        public CommonAPI.Href Html { get; set; }

        [JsonProperty(PropertyName = "workItemType")]
        public CommonAPI.Href WorkItemType { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public CommonAPI.Href Fields { get; set; }

        [JsonProperty(PropertyName = "editor")]
        public CommonAPI.Href Editor { get; set; }

        [JsonProperty(PropertyName = "timeline")]
        public CommonAPI.Href TimeLine { get; set; }

    }
}
