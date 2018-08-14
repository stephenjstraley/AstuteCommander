namespace AstuteCommander.Models
{
    public class FileSourceCodeVM
    {
        public string FileName { get; set; }
        public string SourceCode { get; set; }
        public string FileType { get; set; } = "text";
        public string ErrorMessage { get; set; }

    }
}
