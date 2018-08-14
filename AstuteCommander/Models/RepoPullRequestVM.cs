using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Models
{
    public class RepoPullRequestVM
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string SourceBranch { get; set; }
        public string TargetBranch { get; set; }
        public string MergeStatus { get; set; }
        public int WorkItems { get; set; } = 0;
    }
}
