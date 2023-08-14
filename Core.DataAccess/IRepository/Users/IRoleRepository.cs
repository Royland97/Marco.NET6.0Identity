using Core.Domain.Users;

namespace Core.DataAccess.IRepository.Users
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
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task SaveRoleAsync(Role role, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateRoleAsync(Role role, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteRoleAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        Task<Role> GetRoleByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a List of Roles
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        List<Role> GetAllByIdsRoles(List<int> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Gets All Roles by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Role>> GetAllRoleByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
