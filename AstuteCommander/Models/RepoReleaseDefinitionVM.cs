using System.Collections.Generic;

namespace AstuteCommander.Models
{
    public class RepoReleaseDefinitionVM
    {
        public string Id { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int Revisions { get; set; }
    }
}