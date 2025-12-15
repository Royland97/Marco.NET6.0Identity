using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Authentication
{
    public class AuthenticationModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
