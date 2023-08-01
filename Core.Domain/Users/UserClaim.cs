namespace Core.Domain.Users
{
    /// <summary>
    /// Associate a Claim to a User
    /// </summary>
    public class UserClaim: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the user claim type.
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the user claim value.
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        /// Gets or sets the user claim value type.
        /// </summary>
        public virtual string ClaimValueType { get; set; }

        /// <summary>
        /// Gets or sets the user claim issuer.
        /// </summary>
        public virtual string ClaimIssuer { get; set; }

        /// <summary>
        /// Gets or sets the user than owns this claim.
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}
