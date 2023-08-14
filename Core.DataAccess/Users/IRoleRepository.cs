using Core.Domain.Users;

namespace Core.DataAccess.Users
{
    /// <summary>
    /// Role Repository Interface
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Saves a new Role
        /// </summary>
        /// <param name="role"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task SaveRoleAsync(Role role);

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="role"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateRoleAsync(Role role);

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteRoleAsync(int id);

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        Task<Role> GetRoleByIdAsync(int id);

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetAllRolesAsync();

        /// <summary>
        /// Gets a List of Roles
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<Role> GetAllByIdsRoles(List<int> ids);

        /// <summary>
        /// Gets All Roles by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<Role>> GetAllRoleByIdsAsync(List<int> ids);
    }
}
