using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an application role.
    /// </summary>
    [Table("AspNetRoles")]
    public class Role : Entity
    {
        #region Constants

        /// <summary>
        /// Gets the name of the built-in role for Administrator users.
        /// </summary>
        public const string Administrator = "Administrator";

        #endregion

        #region Fields

        /// <summary>
        /// Gets or sets the name of the role. The name must be unique across all roles.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name of the role. The normalized name must be unique across all roles.
        /// </summary>
        public string NormalizedName { get; set; }

        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether this role is one of the default system roles. System roles can not be
        /// deleted.
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Define whether the role is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Resources

        /// <summary>
        /// Gets the Role's Resources
        /// </summary>
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();

        #endregion

        #region Claims

        /// <summary>
        /// Gets the Role's Claims
        /// </summary>
        public ICollection<RoleClaim> RoleClaims { get; set; } = new List<RoleClaim>();

        #endregion

        #region Users
        
        /// <summary>
        /// Gets the Role's Users
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();

        #endregion

        #endregion
    }
}
