using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// Resource Repository
    /// </summary>
    public class ResourceRepository: IResourceRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ResourceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Saves a new Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveResourceAsync(Resource resource)
        {
            await context.Resources.AddAsync(resource);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves a List of Resources
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public async Task SaveAllResourcesAsync(List<Resource> resources)
        {
            foreach (Resource resource in resources)
                await SaveResourceAsync(resource);
        }

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateResourceAsync(Resource resource, CancellationToken cancellationToken)
        {
            context.Entry(resource).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteResourceAsync(int id, CancellationToken cancellationToken)
        {
            Resource resource = await GetResourceByIdAsync(id, cancellationToken);

            context.Resources.Remove(resource);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<Resource> GetResourceByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Resources.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Resource>> GetAllResourcesAsync(CancellationToken cancellationToken)
        {
            return await context.Resources.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets All Resources by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Resource>> GetAllResourceByIdsAsync(List<int> ids, CancellationToken cancellationToken) 
        { 
            List<Resource> resources = new();

            foreach (int id in ids)
            {
                var resource = await GetResourceByIdAsync(id, cancellationToken);
                if(resource != null)
                    resources.Add(resource);
            }

            return resources;
        }

    }
}
