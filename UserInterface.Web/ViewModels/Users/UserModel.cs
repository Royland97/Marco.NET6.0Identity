using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public bool Active { get; set; }

    }

}
