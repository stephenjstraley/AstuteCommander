using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Models
{
    public class RepoCommitVM
    {
        public string Guid { get; set; }
        public string Author { get; set; }
        public string AuthorDate { get; set; }
        public DateTime TheDate { get; set; }
        public string Committer { get; set; }
        public string CommitterDate { get; set; }
        public string Comment { get; set; }

    }
}
