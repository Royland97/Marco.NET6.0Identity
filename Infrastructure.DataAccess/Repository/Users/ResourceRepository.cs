using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// Resource Repository
    /// </summary>
    public class ResourceRepository: GenericRepository<Resource> ,IResourceRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public ResourceRepository(ApplicationDbContext context):
            base(context)
        {
        }

    }
}
