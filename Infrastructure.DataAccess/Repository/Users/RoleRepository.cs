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

        /// <summary>
        /// Gets All Roles by their Ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetAllRoleByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            List<Role> roles = new();

            foreach (int id in ids)
            {
                var role = await GetByIdAsync(id, cancellationToken);
                if (role != null)
                    roles.Add(role);
            }

            return roles;
        }

    }
}
