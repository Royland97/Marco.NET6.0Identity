namespace Core.Domain.Users
{
    /// <summary>
    /// Associate an Authentication Token to a User
    /// </summary>
    public class UserToken: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the LoginProvider this token is from.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the name of the token.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the token value.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Gets or sets the user that owns this user token.
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}
