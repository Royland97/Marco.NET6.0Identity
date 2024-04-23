using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an User Application
    /// </summary>
    public class User: IdentityUser
    {
        #region Fields

        /// <summary>
        /// Defines whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Relations
        
        /// <summary>
        /// Gets the User Claims 
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; set; }
        
        /// <summary>
        /// Gets the User Logins
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; set; }

        /// <summary>
        /// Gets the User Authentication Tokens
        /// </summary>
        public virtual ICollection<UserToken> Tokens { get; set; }
        
        /// <summary>
        /// Gets the User Roles
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
        #endregion

        #endregion
    }
}
