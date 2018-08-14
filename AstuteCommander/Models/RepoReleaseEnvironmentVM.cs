using System.Collections.Generic;

namespace AstuteCommander.Models
{
    public class RepoReleaseEnvironmentVM
    {
        public string DefId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int DaysToKeep { get; set; }
        public int ReleasesToKeep { get; set; }
        public string AppPoolName { get; set; }
        public string MachinesList { get; set; }
        public string ApplicationDirectory { get; set; }
    }
}