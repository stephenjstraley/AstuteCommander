using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsField
    {
        [JsonProperty(PropertyName = "System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonProperty(PropertyName = "System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonProperty(PropertyName = "System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonProperty(PropertyName = "System.WorkItemType")]
        public string WorkItemType { get; set; }

        [JsonProperty(PropertyName = "System.State")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "System.Reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "System.AssignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty(PropertyName = "System.CreatedDate")]
        public string CreatedDate { get; set; }

        [JsonProperty(PropertyName = "System.CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "System.ChangedDate")]
        public string ChangedDate { get; set; }

        [JsonProperty(PropertyName = "System.ChangedBy")]
        public string ChangedBy { get; set; }

        [JsonProperty(PropertyName = "System.Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "System.BoardColumn")]
        public string BoardColumn { get; set; }

        [JsonProperty(PropertyName = "System.BoardColumnDone")]
        public string BoardColumnDone { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.Priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.BacklogPriority")]
        public string BacklogPriority { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Scheduling.Effort")]
        public string Effort { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Scheduling.TargetDate")]
        public string TargetDate { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.BusinessValue")]
        public int BusinessValue { get; set; }

        [JsonProperty(PropertyName = "System.Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

    }
}
