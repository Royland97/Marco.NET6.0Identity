using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool Active { get; set; }

        public List<int> RolesIds { get; set; }

    }

}
