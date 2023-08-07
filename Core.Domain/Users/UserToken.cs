using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Associate an Authentication Token to a User
    /// </summary>
    [Table("AspNetUserTokens")]
    public class UserToken: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the user that owns this user token.
        /// </summary>
        public User User { get; set; }

        #endregion
    }
}
