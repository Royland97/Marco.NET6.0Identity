using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Associate an User to a Login
    /// </summary>
    [Table("AspNetUserLogins")]
    [NotMapped]
    public class UserLogin : Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the provider display name.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user that owns this user login.
        /// </summary>
        public User User { get; set; }

        #endregion
    }
}