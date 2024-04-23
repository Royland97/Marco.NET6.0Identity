using Core.Domain.Users;

namespace Core.DataAccess.IRepository.Users
{
    /// <summary>
    /// Role Repository Interface
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Gets all Roles by Id List 
        /// </summary>
        /// <param name="ids">Id List</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Role>> GetAllByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken);
    }
}
