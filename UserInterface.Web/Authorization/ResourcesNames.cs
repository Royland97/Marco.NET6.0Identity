namespace UserInterface.Web.Authorization
{
    /// <summary>
    /// Contains the names of the routes or the application to avoid "magic strings" duplicated in the source code.
    /// </summary>
    public class ResourcesNames
    {
        #region Users

        /// <summary>
        /// Gets the route name to get all users.
        /// </summary>
        public const string GetAllUsers = nameof(GetAllUsers);

        /// <summary>
        /// Gets the route name to get an user by ID.
        /// </summary>
        public const string GetUserById = nameof(GetUserById);

        /// <summary>
        /// Gets the route name to create a new user.
        /// </summary>
        public const string CreateUser = nameof(CreateUser);

        /// <summary>
        /// Gets the route name to update an user.
        /// </summary>
        public const string UpdateUser = nameof(UpdateUser);

        /// <summary>
        /// Gets the route name to delete an user.
        /// </summary>
        public const string DeleteUser = nameof(DeleteUser);

        /// <summary>
        /// Gets the route name to get all roles by an user Id.
        /// </summary>
        public const string GetRolesByUserId = nameof(GetRolesByUserId);

        /// <summary>
        /// Gets the route name to assign a role to a user.
        /// </summary>
        public const string AssingRoleToUser = nameof(AssingRoleToUser);

        /// <summary>
        /// Gets the route name to revoke a role to a user.
        /// </summary>
        public const string RevokeRoleToUser = nameof(RevokeRoleToUser);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        public const string ChangePassword = nameof(ChangePassword);

        #endregion
    }
}
