using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Users
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
            if (resource == null)
                throw new ArgumentNullException();

            await context.Resources.AddAsync(resource);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateResourceAsync(Resource resource)
        {
            if (resource == null)
                throw new ArgumentNullException();

            context.Entry(resource).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Resource
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteResourceAsync(int id)
        {
            Resource resource = await GetResourceByIdAsync(id);

            context.Resources.Remove(resource);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a Resource by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<Resource> GetResourceByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();

            return await context.Resources.SingleOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Gets All Resources by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<Resource>> GetAllResourceByIdsAsync(List<int> ids) 
        { 
            List<Resource> resources = new();

            foreach (int id in ids)
            {
                var resource = await GetResourceByIdAsync(id);
                if(resource != null)
                    resources.Add(resource);
            }

            return resources;
        }

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Resource>> GetAllResourcesAsync()
        {
            return await context.Resources.ToListAsync();
        }

    }
}
