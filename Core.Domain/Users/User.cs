namespace Core.Domain.Users
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class User: Entity
    {
        #region Fields

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Get or sets whether the email is confirmed or not.
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the user name of this user.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user GUID.
        /// </summary>
        public virtual Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the user phone number.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Get or sets whether the phone number is confirmed or not.
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Define whether the user is active or not in the system.
        /// </summary>
        public virtual bool Active { get; set; }

        #region Identity

        /// <summary>
        /// Gets or sets the user security stamp.
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets whether the two factor authentication is enabled or not.
        /// </summary>
        public virtual bool TwoFactorAuthenticationEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether the user has lockout enabled or not.
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time (in UTC) when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the user access failed count.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the normalized email of this user.
        /// </summary>
        /// 
        /// <remarks>This field is used for identity stores search.</remarks>
        public virtual string NormalizedEmail { get; set; }

        /// <summary>
        /// Gets or sets the normalized user name of this user.
        /// </summary>
        /// 
        /// <remarks>This field is used for identity stores search.</remarks>
        public virtual string NormalizedUserName { get; set; }

        /// <summary>
        /// Gets or sets the user password hash.
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the user password salt.
        /// </summary>
        public virtual byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the user password hash iterations.
        /// </summary>
        public virtual int PasswordHashIterations { get; set; }

        /// <summary>
        /// Gets or sets the user password hash length.
        /// </summary>
        public virtual int PasswordHashLength { get; set; }

        #endregion

        #region Roles

        private readonly ICollection<Role> _roles = new HashSet<Role>();

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        public virtual IEnumerable<Role> Roles => _roles;

        #endregion

        #region Logins

        private readonly ISet<UserLogin> _logins = new HashSet<UserLogin>();

        /// <summary>
        /// Gets the user logins.
        /// </summary>
        public virtual IEnumerable<UserLogin> Logins => _logins;

        #endregion

        #region Claims

        private readonly ISet<UserClaim> _claims = new HashSet<UserClaim>();

        /// <summary>
        /// Gets the user claims.
        /// </summary>
        public virtual IEnumerable<UserClaim> Claims => _claims;

        #endregion

        #region Tokens

        private readonly ISet<UserToken> _tokens = new HashSet<UserToken>();

        /// <summary>
        /// Gets the user authentication tokens.
        /// </summary>
        public virtual IEnumerable<UserToken> Tokens => _tokens;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the user has password or not.
        /// </summary>
        /// 
        /// <returns>true if the user has password, false otherwise</returns>
        public virtual bool HasPassword()
        {
            var hasPassword = !string.IsNullOrWhiteSpace(PasswordHash);

            return hasPassword;
        }

        /// <summary>
        /// Resets the access failed count of the user;
        /// </summary>
        public virtual void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        #region Roles

        /// <summary>
        /// Adds a new role to the role.
        /// </summary>
        /// 
        /// <param name="role">The role to add</param>
        /// 
        /// <returns>true if the role was added, false otherwise</returns>
        public virtual bool AddRole(Role role)
        {
            if (!HasRoleAssigned(role))
            {
                _roles.Add(role);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a collection of roles to the role.
        /// </summary>
        /// 
        /// <param name="roles">The collection of role to add</param>
        public virtual void AddRoles(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                AddRole(role);
            }
        }

        /// <summary>
        /// Delete a role to a role.
        /// </summary>
        /// 
        /// <param name="role">The role to delete</param>
        /// 
        /// <returns>true if the role was added, false otherwise</returns>
        public virtual bool DeleteRole(Role role)
        {
            if (_roles.Remove(role))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete all role list from this role group.
        /// </summary>
        public virtual void DeleteAllRoles()
        {
            foreach (var role in Roles.ToList())
                DeleteRole(role);
        }

        /// <summary>
        /// Revoke an existing role from the user searching the role by name.
        /// </summary>
        /// 
        /// <param name="normalizedRoleName">The normalized name of the role to revoke</param>
        /// 
        /// <returns>true if the role was revoked, false otherwise</returns>
        public virtual bool RevokeRole(string normalizedRoleName)
        {
            var role = GetRole(normalizedRoleName, false);

            if (role == null)
            {
                return false;
            }

            return DeleteRole(role);
        }

        /// <summary>
        /// Determines whether the user has the specified role assigned searching the role by normalized name.
        /// </summary>
        /// 
        /// <param name="normalizedRoleName">The normalized name of the role to check if the user has this role assigned</param>
        /// <param name="checkRoleIsActive">Determines whether should check if the role is active or not</param>
        /// 
        /// <returns>true if the user has the role assigned, false otherwise</returns>
        public virtual bool HasRoleAssigned(string normalizedRoleName, bool checkRoleIsActive = true)
        {
            var role = GetRole(normalizedRoleName, checkRoleIsActive);
            var roleAssigned = role != null;

            return roleAssigned;
        }

        /// <summary>
        /// Gets a role assigned to this user with the provided normalized role name.
        /// </summary>
        /// 
        /// <param name="normalizedRoleName">The normalized name of the role to find</param>
        /// <param name="checkRoleIsActive">Determines whether should check if the role is active or not</param>
        /// 
        /// <returns>Role found with the provided normalized role name, null otherwise</returns>
        public virtual Role GetRole(string normalizedRoleName, bool checkRoleIsActive = true)
        {
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                return null;
            }

            var query = from r in Roles
                        where r.NormalizedName.Equals(normalizedRoleName, StringComparison.OrdinalIgnoreCase)
                              && (!checkRoleIsActive || r.Active)
                        select r;

            var role = query.FirstOrDefault();

            return role;
        }

        /// <summary>
        /// Determines whether the customer has the specified role assigned.
        /// </summary>
        /// 
        /// <param name="role">The role to check if the customer has this role assigned</param>
        /// 
        /// <returns>true if the customer has the role assigned, false otherwise</returns>
        public virtual bool HasRoleAssigned(Role role)
        {
            return role != null && HasRoleAssigned(role.Name);
        }

        /// <summary>
        /// Determines whether the customer has the specified role assigned searching the role by it's name.
        /// </summary>
        /// 
        /// <param name="roleName">The name of the role to check if the customer has this role assigned</param>
        /// 
        /// <returns>true if the customer has the role assigned, false otherwise</returns>
        public virtual bool HasRoleAssigned(string roleName)
        {
            return !string.IsNullOrWhiteSpace(roleName) && Roles.Any(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Logins

        /// <summary>
        /// Adds a new user login to the user.
        /// </summary>
        /// 
        /// <param name="userLogin">The user login to add</param>
        /// 
        /// <returns>true if the user login was added, false otherwise</returns>
        public virtual bool AddLogin(UserLogin userLogin)
        {
            if (_logins.Add(userLogin))
            {
                userLogin.User = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an existing user login from the user.
        /// </summary>
        /// 
        /// <param name="userLogin">The user login to delete</param>
        /// 
        /// <returns>true if the user login was deleted, false otherwise</returns>
        public virtual bool DeleteLogin(UserLogin userLogin)
        {
            if (_logins.Remove(userLogin))
            {
                userLogin.User = null;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an existing user login from the user searching the user login by the <paramref name="loginProvider"/>
        /// and the <paramref name="providerKey"/> values.
        /// </summary>
        /// 
        /// <param name="loginProvider">Login provider</param>
        /// <param name="providerKey">Provider key</param>
        /// 
        /// <returns>true if the user login was deleted, false otherwise</returns>
        public virtual bool DeleteLogin(string loginProvider, string providerKey)
        {
            var userLogin = GetLogin(loginProvider, providerKey);

            if (userLogin == null)
            {
                return false;
            }

            return DeleteLogin(userLogin);
        }

        /// <summary>
        /// Gets an user login linked to this user searching the user login by the <paramref name="loginProvider"/>
        /// and the <paramref name="providerKey"/> values.
        /// </summary>
        /// 
        /// <param name="loginProvider">Login provider</param>
        /// <param name="providerKey">Provider key</param>
        /// 
        /// <returns>
        /// User login found with the provided <paramref name="loginProvider"/> and <paramref name="providerKey"/> values,
        /// null otherwise
        /// </returns>
        public virtual UserLogin GetLogin(string loginProvider, string providerKey)
        {
            if (string.IsNullOrWhiteSpace(loginProvider) || string.IsNullOrWhiteSpace(providerKey))
            {
                return null;
            }

            var query = from ul in Logins
                        where ul.LoginProvider.Equals(loginProvider, StringComparison.OrdinalIgnoreCase)
                              && ul.ProviderKey.Equals(providerKey, StringComparison.OrdinalIgnoreCase)
                        select ul;

            var userLogin = query.FirstOrDefault();

            return userLogin;
        }

        #endregion

        #region Claims

        /// <summary>
        /// Adds a collection of user claims to the user.
        /// </summary>
        /// 
        /// <param name="userClaims">The collection of user claims to add</param>
        public virtual void AddClaims(IEnumerable<UserClaim> userClaims)
        {
            foreach (var userClaim in userClaims)
            {
                AddClaim(userClaim);
            }
        }

        /// <summary>
        /// Adds a new user claim to the user.
        /// </summary>
        /// 
        /// <param name="userClaim">The user claim to add</param>
        /// 
        /// <returns>true if the user claim was added, false otherwise</returns>
        public virtual bool AddClaim(UserClaim userClaim)
        {
            if (_claims.Add(userClaim))
            {
                userClaim.User = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Replaces the claim searching the user claim by the <paramref name="claimType"/>
        /// and the <paramref name="claimValue"/> values and is replaced by the <paramref name="newUserClaim"/>. 
        /// </summary>
        /// 
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// <param name="newUserClaim">New user claim to replace the old claim</param>
        public virtual void ReplaceClaim(
            string claimType,
            string claimValue,
            UserClaim newUserClaim)
        {
            DeleteClaim(claimType, claimValue);
            AddClaim(newUserClaim);
        }

        /// <summary>
        /// Removes an existing user claim from the user.
        /// </summary>
        /// 
        /// <param name="userClaim">The user claim to delete</param>
        /// 
        /// <returns>true if the user claim was deleted, false otherwise</returns>
        public virtual bool DeleteClaim(UserClaim userClaim)
        {
            if (_claims.Remove(userClaim))
            {
                userClaim.User = null;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an existing user claim from the user searching the user claim by the <paramref name="claimType"/>
        /// and the <paramref name="claimValue"/> values.
        /// </summary>
        /// 
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// 
        /// <returns>true if the user claim was deleted, false otherwise</returns>
        public virtual bool DeleteClaim(string claimType, string claimValue)
        {
            var userClaim = GetClaim(claimType, claimValue);

            if (userClaim == null)
            {
                return false;
            }

            return DeleteClaim(userClaim);
        }

        /// <summary>
        /// Gets an user claim searching the user claim by the <paramref name="claimType"/>
        /// and the <paramref name="claimValue"/> values.
        /// </summary>
        /// 
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        /// 
        /// <returns>
        /// User claim found with the provided <paramref name="claimType"/> and <paramref name="claimValue"/> values,
        /// null otherwise
        /// </returns>
        public virtual UserClaim GetClaim(string claimType, string claimValue)
        {
            if (string.IsNullOrWhiteSpace(claimType) || string.IsNullOrWhiteSpace(claimValue))
            {
                return null;
            }

            var query = from uc in Claims
                        where uc.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase)
                              && uc.ClaimValue.Equals(claimValue, StringComparison.OrdinalIgnoreCase)
                        select uc;

            var userClaim = query.FirstOrDefault();

            return userClaim;
        }

        #endregion

        #region Tokens

        /// <summary>
        /// Adds a new user token to the user.
        /// </summary>
        /// 
        /// <param name="userToken">The user token to add</param>
        /// 
        /// <returns>true if the user token was added, false otherwise</returns>
        public virtual bool AddToken(UserToken userToken)
        {
            if (_tokens.Add(userToken))
            {
                userToken.User = this;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an existing user token from the user.
        /// </summary>
        /// 
        /// <param name="userToken">The user token to delete</param>
        /// 
        /// <returns>true if the user token was deleted, false otherwise</returns>
        public virtual bool DeleteToken(UserToken userToken)
        {
            if (_tokens.Remove(userToken))
            {
                userToken.User = null;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes an existing user token from the user searching the user token by the <paramref name="loginProvider"/>
        /// and the <paramref name="name"/> values.
        /// </summary>
        /// 
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// 
        /// <returns>true if the user token was deleted, false otherwise</returns>
        public virtual bool DeleteToken(string loginProvider, string name)
        {
            var userToken = GetToken(loginProvider, name);

            if (userToken == null)
            {
                return false;
            }

            return DeleteToken(userToken);
        }

        /// <summary>
        /// Gets an user token linked to this user searching the user token by the <paramref name="loginProvider"/>
        /// and the <paramref name="name"/> values.
        /// </summary>
        /// 
        /// <param name="loginProvider">The authentication provider for the token.</param>
        /// <param name="name">The name of the token.</param>
        /// 
        /// <returns>
        /// User token found with the provided <paramref name="loginProvider"/> and <paramref name="name"/> values,
        /// null otherwise
        /// </returns>
        public virtual UserToken GetToken(string loginProvider, string name)
        {
            if (string.IsNullOrWhiteSpace(loginProvider) || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var query = from ut in Tokens
                        where ut.LoginProvider.Equals(loginProvider, StringComparison.OrdinalIgnoreCase)
                              && ut.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                        select ut;

            var userToken = query.FirstOrDefault();

            return userToken;
        }

        #endregion

        #endregion
    }
}
