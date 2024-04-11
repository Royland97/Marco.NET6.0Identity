using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a resource that must be access protected by the system.
    /// </summary>
    [Table("AspNetResource")]
    public class Resource: Entity
    {
        #region Fields

        /// <summary>
        /// Resource name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Resource description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        #region Relationships

        /// <summary>
        /// Gets the Resource Roles
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        #endregion
    }
}