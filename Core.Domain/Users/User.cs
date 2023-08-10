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
        /// Get or sets whether the email is confirmed or not.
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the user name of this user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user GUID.
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the user phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Get or sets whether the phone number is confirmed or not.
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Define whether the user is active or not in the system.
        /// </summary>
        public bool Active { get; set; }

        #region Identity
        /*
        /// <summary>
        /// Gets or sets the user security stamp.
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets whether the two factor authentication is enabled or not.
        /// </summary>
        public bool TwoFactorAuthenticationEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether the user has lockout enabled or not.
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time (in UTC) when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the user access failed count.
        /// </summary>
        public int AccessFailedCount { get; set; }
        */
        /// <summary>
        /// Gets or sets the normalized email of this user.
        /// </summary>
        /// 
        /// <remarks>This field is used for identity stores search.</remarks>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets the normalized user name of this user.
        /// </summary>
        /// 
        /// <remarks>This field is used for identity stores search.</remarks>
        public string NormalizedUserName { get; set; }
        /*
        /// <summary>
        /// Gets or sets the user password hash.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the user password salt.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the user password hash iterations.
        /// </summary>
        public int PasswordHashIterations { get; set; }

        /// <summary>
        /// Gets or sets the user password hash length.
        /// </summary>
        public int PasswordHashLength { get; set; }*/

        #endregion

        /// <summary>
        /// Gets the User's Roles
        /// </summary>
        public ICollection<Role> Roles { get; } = new List<Role>();

        /// <summary>
        /// Gets the User's Logins
        /// </summary>
        public ICollection<UserLogin> UserLogins { get; } = new List<UserLogin>();

        /// <summary>
        /// Gets the User's Claims
        /// </summary>
        public ICollection<UserClaim> UserClaims { get; } = new List<UserClaim>();

        /// <summary>
        /// Gets the User's Authentication Tokens
        /// </summary>
        public ICollection<UserToken> UserTokens { get; } = new List<UserToken>();
    }
}
