namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an application role.
    /// </summary>
    public class Role : Entity
    {
        #region Constants

        /// <summary>
        /// Gets the name of the built-in role for Administrator users.
        /// </summary>
        public const string Administrator = "Administrator";

        #endregion

        #region Fields

        /// <summary>
        /// Gets or sets the name of the role. The name must be unique across all roles.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the normalized name of the role. The normalized name must be unique across all roles.
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets whether this role is one of the default system roles. System roles can not be
        /// deleted.
        /// </summary>
        public virtual bool IsSystemRole { get; set; }

        /// <summary>
        /// Define whether the role is active or not in the system.
        /// </summary>
        public virtual bool Active { get; set; }

        #region Resources

        private readonly ICollection<Resource> _resources = new HashSet<Resource>();

        /// <summary>
        /// Gets the resources of this role.
        /// </summary>
        public virtual IEnumerable<Resource> Resources => _resources;

        #endregion

        #region Claims

        private readonly ISet<RoleClaim> _claims = new HashSet<RoleClaim>();

        /// <summary>
        /// Gets the role claims.
        /// </summary>
        public virtual IEnumerable<RoleClaim> Claims => _claims;

        #endregion

        #region Users

        private readonly ICollection<User> _users = new HashSet<User>();

        /// <summary>
        /// Gets the users of this role.
        /// </summary>
        public virtual IEnumerable<User> Users => _users;

        #endregion

        #endregion

        #region Methods

        #region Resource

        /// <summary>
        /// Adds a new resource to the role.
        /// </summary>
        /// 
        /// <param name="resource">The resource to add</param>
        /// 
        /// <returns>true if the resource was added, false otherwise</returns>
        public virtual bool AddResource(Resource resource)
        {
            if (!HasResourceAssigned(resource))
            {
                _resources.Add(resource);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a collection of resources to the role.
        /// </summary>
        /// 
        /// <param name="resources">The collection of resource to add</param>
        public virtual void AddResources(IEnumerable<Resource> resources)
        {
            foreach (var resource in resources)
            {
                AddResource(resource);
            }
        }

        /// <summary>
        /// Delete a resource to a role.
        /// </summary>
        /// 
        /// <param name="resource">The resource to delete</param>
        /// 
        /// <returns>true if the resource was added, false otherwise</returns>
        public virtual bool DeleteResource(Resource resource)
        {
            if (_resources.Remove(resource))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete all resource list from this resource group.
        /// </summary>
        public virtual void DeleteAllResources()
        {
            foreach (var resource in Resources.ToList())
                DeleteResource(resource);
        }

        /// <summary>
        /// Determines whether the customer has the specified role assigned.
        /// </summary>
        /// 
        /// <param name="resource">The role to check if the customer has this role assigned</param>
        /// 
        /// <returns>true if the customer has the role assigned, false otherwise</returns>
        public virtual bool HasResourceAssigned(Resource resource)
        {
            return resource != null && HasResourceAssigned(resource.Name);
        }

        /// <summary>
        /// Determines whether the customer has the specified role assigned searching the role by it's name.
        /// </summary>
        /// 
        /// <param name="resourceName">The name of the role to check if the customer has this role assigned</param>
        /// 
        /// <returns>true if the customer has the role assigned, false otherwise</returns>
        public virtual bool HasResourceAssigned(string resourceName)
        {
            return !string.IsNullOrWhiteSpace(resourceName) && Resources.Any(r => r.Name.Equals(resourceName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Claims

        /// <summary>
        /// Adds a collection of role claims to the role.
        /// </summary>
        /// 
        /// <param name="roleClaims">The collection of role claims to add</param>
        public virtual void AddClaims(IEnumerable<RoleClaim> roleClaims)
        {
            foreach (var roleClaim in roleClaims)
            {
                AddClaim(roleClaim);
            }
        }

        /// <summary>
        /// Adds a new role claim to the role.
        /// </summary>
        /// 
        /// <param name="roleClaim">The role claim to add</param>
        /// 
        /// <returns>true if the role claim was added, false otherwise</returns>
        public virtual bool AddClaim(RoleClaim roleClaim)
        {
            if (_claims.Add(roleClaim))
            {
                roleClaim.Role = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes an existing role claim from the role.
        /// </summary>
        /// 
        /// <param name="roleClaim">The role claim to delete</param>
        /// 
        /// <returns>true if the role claim was deleted, false otherwise</returns>
        public virtual bool DeleteClaim(RoleClaim roleClaim)
        {
            if (_claims.Remove(roleClaim))
            {
                roleClaim.Role = null;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes an existing role claim from the role searching the role claim by the <paramref name="claimType"/>
        /// and the <paramref name="claimValue"/> values.
        /// </summary>
        /// 
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// 
        /// <returns>true if the role claim was deleted, false otherwise</returns>
        public virtual bool DeleteClaim(string claimType, string claimValue)
        {
            var roleClaim = GetClaim(claimType, claimValue);

            if (roleClaim == null)
            {
                return false;
            }

            return DeleteClaim(roleClaim);
        }

        /// <summary>
        /// Gets a role claim searching the role claim by the <paramref name="claimType"/>
        /// and the <paramref name="claimValue"/> values.
        /// </summary>
        /// 
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// 
        /// <returns>
        /// Role claim found with the provided <paramref name="claimType"/> and <paramref name="claimValue"/> values,
        /// null otherwise
        /// </returns>
        public virtual RoleClaim GetClaim(string claimType, string claimValue)
        {
            if (string.IsNullOrWhiteSpace(claimType) || string.IsNullOrWhiteSpace(claimValue))
            {
                return null;
            }

            var query = from rc in Claims
                        where rc.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase)
                              && rc.ClaimValue.Equals(claimValue, StringComparison.OrdinalIgnoreCase)
                        select rc;

            var roleClaim = query.FirstOrDefault();

            return roleClaim;
        }

        #endregion

        #endregion
    }
}
