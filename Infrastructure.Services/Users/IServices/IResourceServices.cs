using Infrastructure.Services.Users.Models;

namespace Infrastructure.Services.Users.IServices
{
    /// <summary>
    /// Resource Services Interface
    /// </summary>
    public interface IResourceServices
    {
        /// <summary>
        /// Saves a Resource
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveResourceAsync(ResourceModel resourceModel, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateResourceAsync(ResourceModel resourceModel, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteResourceAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets All Resources
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ResourceModel>> GetAllResourcesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets an Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResourceModel> GetResourceByIdAsync(int id, CancellationToken cancellationToken);
    }
}
