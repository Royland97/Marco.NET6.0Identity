namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a Claim granted to all users within a role
    /// </summary>
    public class RoleClaim: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the role claim type.
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the role claim value.
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the role claim value type.
        /// </summary>
        public virtual string ClaimValueType { get; set; }

        /// <summary>
        /// Gets or sets the role claim issuer.
        /// </summary>
        public virtual string ClaimIssuer { get; set; }

        /// <summary>
        /// Gets or sets the role than owns this claim.
        /// </summary>
        public virtual Role Role { get; set; }

        #endregion
    }
}
