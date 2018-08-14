using System.Collections.Generic;
using Newtonsoft.Json;

namespace AstuteCommander.Classes.VSTS
{
    public class VstsApprovals
    {
        [JsonProperty(PropertyName = "approvals")]
        public List<VstsApproval> Approvals { get; set; }
    }
}
