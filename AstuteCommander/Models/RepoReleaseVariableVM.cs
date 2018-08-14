using System.Collections.Generic;

namespace AstuteCommander.Models
{
    public class RepoReleaseVariableVM
    {
        public List<VSTSDataColumn> Columns { get; set; }
        public List<Dictionary<string, string>> Data { get; set; }
    }

}