using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// Role Repository
    /// </summary>
    public class RoleRepository: IRoleRepository
    {
        protected ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets all Roles by Id List 
        /// </summary>
        /// <param name="ids">Id List</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetAllByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            List<Role> roles = new();

            foreach (var id in ids)
                roles.Add(await context.Roles.FindAsync(new object?[] { id }, cancellationToken));

            return roles;
        }
    }
}
