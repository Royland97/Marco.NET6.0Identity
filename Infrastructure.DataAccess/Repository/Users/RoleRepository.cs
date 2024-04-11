using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// Role Repository
    /// </summary>
    public class RoleRepository: GenericRepository<Role> , IRoleRepository
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(ApplicationDbContext context):
            base (context)
        {
        }

    }
}
