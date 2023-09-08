using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
{
    /// <summary>
    /// Role Repository
    /// </summary>
    public class RoleRepository: IRoleRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Saves a new Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveRoleAsync(Role role, CancellationToken cancellationToken)
        {
            await context.Roles.AddAsync(role, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            context.Entry(role).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteRoleAsync(int id, CancellationToken cancellationToken)
        {
            Role role = await GetRoleByIdAsync(id, cancellationToken);

            context.Roles.Remove(role);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<Role> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Roles.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return await context.Roles.ToListAsync(cancellationToken);
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
                var role = await GetRoleByIdAsync(id, cancellationToken);
                if (role != null)
                    roles.Add(role);
            }

            return roles;
        }

    }
}
