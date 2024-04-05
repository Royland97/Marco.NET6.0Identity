using Core.Domain.Users;

namespace Core.DataAccess.IRepository.Users
{
    /// <summary>
    /// Role Repository Interface
    /// </summary>
    public interface IRoleRepository: IGenericRepository<Role>
    {
        /// <summary>
        /// Gets All Roles by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Role>> GetAllRoleByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
