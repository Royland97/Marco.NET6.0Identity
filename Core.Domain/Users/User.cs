using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represent an User Application
    /// </summary>
    public class User: IdentityUser
    {
        #region Fields

        /// <summary>
        /// Define whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Relationships
        
        /// <summary>
        /// Gets the User's Claims 
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; set; }
        
        /// <summary>
        /// Gets the User's Logins
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; set; }

        /// <summary>
        /// Get's the User's Authentication Tokens
        /// </summary>
        public virtual ICollection<UserToken> Tokens { get; set; }
        
        /// <summary>
        /// Get's the User's Roles
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        #endregion

        #endregion
    }
}
