
namespace AstuteCommander.Models
{
    public class InventoryVM
    {
        public string Project { get; set; }
        public string Repository { get; set; }
        public string Extension { get; set; }
        public int FileCount { get; set; }
        public int TotalLines { get; set; }
        public string OtherFile { get; set; }
    }
}
