using AutoMapper;
using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.Services.Users.IServices;
using Infrastructure.Services.Users.Models;

namespace Infrastructure.Services.Users.Services
{
    /// <summary>
    /// Resource Services
    /// </summary>
    public class ResourceServices: IResourceServices
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceServices"/> class.
        /// </summary>
        /// <param name="resourceRepository"></param>
        /// <param name="mapper"></param>
        public ResourceServices(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves a Resource
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveResourceAsync(ResourceModel resourceModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var resource = _mapper.Map<Resource>(resourceModel);
            await _resourceRepository.SaveResourceAsync(resource);
        }

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resourceModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateResourceAsync(ResourceModel resourceModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var resource = _mapper.Map<Resource>(resourceModel);
            await _resourceRepository.UpdateResourceAsync(resource);
        }

        /// <summary>
        /// Deletes a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteResourceAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _resourceRepository.DeleteResourceAsync(id);
        }

        /// <summary>
        /// Gets All Resources
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<ResourceModel>> GetAllResourcesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _resourceRepository.GetAllResourcesAsync();
            return _mapper.Map<List<ResourceModel>>(result);
        }

        /// <summary>
        /// Gets an Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResourceModel> GetResourceByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Resource resource = await _resourceRepository.GetResourceByIdAsync(id);
            return _mapper.Map<ResourceModel>(resource);
        }
    }
}
