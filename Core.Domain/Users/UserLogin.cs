namespace Core.Domain.Users
{
    /// <summary>
    /// Associate an User to a Login
    /// </summary>
    public class UserLogin : Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the login provider.
        /// </summary>
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Gets or sets the provider key.
        /// </summary>
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// Gets or sets the provider display name.
        /// </summary>
        public virtual string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user that owns this user login.
        /// </summary>
        public virtual User User { get; set; }

        #endregion
    }
}