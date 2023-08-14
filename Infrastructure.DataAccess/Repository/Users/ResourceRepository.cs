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
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveResourceAsync(Resource resource, CancellationToken cancellationToken)
        {
            if (resource == null)
                throw new ArgumentNullException();

            await context.Resources.AddAsync(resource, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateResourceAsync(Resource resource, CancellationToken cancellationToken)
        {
            if (resource == null)
                throw new ArgumentNullException();

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
            if (id < 0)
                throw new ArgumentNullException();

            return await context.Resources.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Gets All Resources by their Ids
        /// </summary>
        /// <param name="ids"></param>
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

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Resource>> GetAllResourcesAsync(CancellationToken cancellationToken)
        {
            return await context.Resources.ToListAsync(cancellationToken);
        }

    }
}
