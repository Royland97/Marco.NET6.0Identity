using Core.Domain.Users;

namespace Core.DataAccess.Users
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
        /// <exception cref="ArgumentNullException"></exception>
        Task SaveResourceAsync(Resource resource);

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateResourceAsync(Resource resource);

        /// <summary>
        /// Delete a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteResourceAsync(int id);

        /// <summary>
        /// Gets a Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        Task<Resource> GetResourceByIdAsync(int id);

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Resource>> GetAllResourcesAsync();

        /// <summary>
        /// Gets All Resources by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<Resource>> GetAllResourceByIdsAsync(List<int> ids);
    }
}
