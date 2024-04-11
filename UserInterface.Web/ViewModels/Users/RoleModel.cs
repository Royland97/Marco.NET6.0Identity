using System.ComponentModel.DataAnnotations;

namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// Role Model
    /// </summary>
    public class RoleModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public bool Active { get; set; }
    }
}
