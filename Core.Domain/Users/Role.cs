using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an application role.
    /// </summary>
    public class Role: IdentityRole
    {
        /// <summary>
        /// Get the name to build the Administrator Role
        /// </summary>
        public const string Admin = "Admin";

        #region Fields

        /// <summary>
        /// Role name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Role description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Defines whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Relations
        
        /// <summary>
        /// Gets the Role Users
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets the Role Claims
        /// </summary>
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Gets the Role Resources
        /// </summary>
        public virtual ICollection<Resource> Resources { get; set; }

        #endregion

        #endregion
    }
}
