using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represent an application role.
    /// </summary>
    public class Role: IdentityRole
    {
        #region Fields

        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Define whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Relationships
        
        /// <summary>
        /// Get's the Role's Users
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Get's the Role's Claims
        /// </summary>
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        /// <summary>
        /// Gets the Role's Resources
        /// </summary>
        public virtual ICollection<Resource> Resources { get; set; }

        #endregion

        #endregion
    }
}
