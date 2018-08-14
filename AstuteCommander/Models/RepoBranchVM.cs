using System.Collections.Generic;

namespace AstuteCommander.Models
{
    public class RepoBranchVM
    {
        public string RepoGuid { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public bool IsLocked { get; set; }
        public string LockedBy { get; set; }
        public bool HasStats { get; set; }
        public bool IsCurrentBranch { get; set; }
        public List<RepoBranchStatusVM> Statuses { get; set; }
    }

    public class RepoBranchStatusVM
    {
        public string Id { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}