using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander.Models
{
    public class LastCommitsVM
    {
        public string RepoGuid { get; set; }
        public string RepoName { get; set; }
        public string Author { get; set; }
        public string AuthorDate { get; set; }
        public string Committer { get; set; }
        public string CommitterDate { get; set; }
        public string Comment { get; set; }
        public int Adds { get; set; }
        public int Edits { get; set; }
        public int Deletes { get; set; }
    }
}
