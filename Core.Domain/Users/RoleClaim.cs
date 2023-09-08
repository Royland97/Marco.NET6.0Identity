using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a Claim granted to all users within a role
    /// </summary>
    [Table("AspNetRoleClaims")]
    [NotMapped]
    public class RoleClaim: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the role claim type.
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the role claim value.
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the role claim value type.
        /// </summary>
        public string ClaimValueType { get; set; }

        /// <summary>
        /// Gets or sets the role claim issuer.
        /// </summary>
        public string ClaimIssuer { get; set; }

        /// <summary>
        /// Gets or sets the role than owns this claim.
        /// </summary>
        public Role Role { get; set; }

        #endregion
    }
}
