using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsPullRequest
    {
        [JsonProperty(PropertyName = "repository")]
        public VstsRepository Repository { get; set; }

        [JsonProperty(PropertyName = "pullRequestId")]
        public string PRId { get; set; }

        [JsonProperty(PropertyName = "codeReviewId")]
        public string CRId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public VstsBy CreatedBy { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "sourceRefName")]
        public string SourceBranch { get; set; }

        [JsonProperty(PropertyName = "targetRefName")]
        public string TargetBranch { get; set; }

        [JsonProperty(PropertyName = "mergeStatus")]
        public string MergeStatus { get; set; }

        [JsonProperty(PropertyName = "mergeId")]
        public string MergeId { get; set; }

        [JsonProperty(PropertyName = "lastMergeSourceCommit")]
        public VstsCommit LastMergeSourceCommit { get; set; }

        [JsonProperty(PropertyName = "lastMergeTargetCommit")]
        public VstsCommit LastMergeTargetCommit { get; set; }

        [JsonProperty(PropertyName = "lastMergeCommit")]
        public VstsCommit LastMergeCommit { get; set; }

        [JsonProperty(PropertyName = "reviewers")]
        public List<VstsReviewer> Reviewers { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "supportsIterations")]
        public string SupportsItterations { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public VstsLinks Links { get; set; }

        [JsonProperty(PropertyName = "artifactId")]
        public string ArtifactId { get; set; }

    }
}
