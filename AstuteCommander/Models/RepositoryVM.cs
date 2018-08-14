using System;

namespace AstuteCommander.Models
{
    public class RepositoryVM
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string DefaultBranch { get; set; }
        public DateTime? BuildLastDate { get; set; }
        public int BuildCount { get; set; } = 0;
        public DateTime? CommitLastDate { get; set; }
        public int CommitCount { get; set; } = 0;
        public DateTime? PushLastDate { get; set; }
        public int PushCount { get; set; } = 0;
        public DateTime? PullLastDate { get; set; }
        public int PullCount { get; set; } = 0;
        public string PBI { get; set; }
        public string OverridingName { get; set; }
        public bool OffRelease { get; set; }
        public bool DirectoryOnDisk { get; set; }
    }
}
