using Infrastructure.Services.Users.Models;

namespace Infrastructure.Services.Users.IServices
{
    /// <summary>
    /// Role Services Interface
    /// </summary>
    public interface IRoleServices
    {
        /// <summary>
        /// Saves a Role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveRoleAsync(RoleModel roleModel, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateRoleAsync(RoleModel roleModel, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteRoleAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets All Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<RoleModelList>> GetAllRolesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RoleModelList> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
    }
}
