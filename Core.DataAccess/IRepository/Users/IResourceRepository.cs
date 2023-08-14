using Core.Domain.Users;

namespace Core.DataAccess.IRepository.Users
{
    /// <summary>
    /// Resource Repository Interface
    /// </summary>
    public interface IResourceRepository
    {
        /// <summary>
        /// Saves a new Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task SaveResourceAsync(Resource resource, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateResourceAsync(Resource resource, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteResourceAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        Task<Resource> GetResourceByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Resource>> GetAllResourcesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets All Resources by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Resource>> GetAllResourceByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
