using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a resource that must be access protected by the system.
    /// </summary>
    [Table("AspNetResources")]
    public class Resource: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource description.
        /// </summary>
        public string Description { get; set; }

        #region Roles

        /// <summary>
        /// Gets the Resource's Roles
        /// </summary>
        public List<Role> Roles { get; set; } = new List<Role>();

        #endregion

        #endregion
    }
}