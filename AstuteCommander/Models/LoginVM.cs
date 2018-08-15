using System.ComponentModel.DataAnnotations;

namespace AstuteCommander.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You need a valid user name on the network")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        //[Required]
        //public string PersonalAccessToken { get; set; }

        //[Required]
        //public string LocalVSTSFolder { get; set; }

        //[Required]
        //public string GitEXEFileLocation { get; set; }

        //[Required]
        //public string TempDir { get; set; }

    }
}
