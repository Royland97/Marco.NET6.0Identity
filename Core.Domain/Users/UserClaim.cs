using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Associate a Claim to a User
    /// </summary>
    [Table("AspNetUserClaims")]
    public class UserClaim: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the user claim type.
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the user claim value.
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the user claim value type.
        /// </summary>
        public string ClaimValueType { get; set; }

        /// <summary>
        /// Gets or sets the user claim issuer.
        /// </summary>
        public string ClaimIssuer { get; set; }

        /// <summary>
        /// Gets or sets the user than owns this claim.
        /// </summary>
        public User User { get; set; } = null!;

        #endregion
    }
}
