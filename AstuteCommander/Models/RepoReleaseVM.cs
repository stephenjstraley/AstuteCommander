using System.Collections.Generic;

namespace AstuteCommander.Models
{
    public class RepoReleaseVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string VstsLink { get; set; }
    }
}