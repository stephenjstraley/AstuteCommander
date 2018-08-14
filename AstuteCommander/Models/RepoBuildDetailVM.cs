using System;

namespace AstuteCommander.Models
{
    public class RepoBuildDetailVM
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string Build { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string VstsLink { get; set; }
    }
}
