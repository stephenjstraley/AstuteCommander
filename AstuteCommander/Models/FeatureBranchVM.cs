using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Models
{
    public class FeatureBranchVM
    {
        public string RepoGuid { get; set; }
        public string RepoName { get; set; }
        public string BranchName { get; set; }
        public string BranchObjectId { get; set; }
    }
}
