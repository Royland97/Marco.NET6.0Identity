using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    [Table("AspNetUsers")]
    public class User: Entity
    {
        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user name of this user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Define whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Roles

        /// <summary>
        /// Gets the User's Roles
        /// </summary>
        public ICollection<Role> Roles { get; set; } = new List<Role>();

        #endregion

        /// <summary>
        /// Gets the User's Logins
        /// </summary>
        public ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

        /// <summary>
        /// Gets the User's Claims
        /// </summary>
        public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

        /// <summary>
        /// Gets the User's Authentication Tokens
        /// </summary>
        public ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    }
}
