namespace Core.Domain.Users
{
    /// <summary>
    /// Represents a resource that must be access protected by the system.
    /// </summary>
    public class Resource: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource description.
        /// </summary>
        public virtual string Description { get; set; }

        #region Roles

        private readonly ICollection<Role> _roles = new HashSet<Role>();

        /// <summary>
        /// Gets the roles of this Resource.
        /// </summary>
        public virtual IEnumerable<Role> Roles => _roles;

        #endregion

        #endregion
    }
}