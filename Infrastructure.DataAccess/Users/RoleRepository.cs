using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Users
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
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveRoleAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException();

            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="role"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateRoleAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException();

            context.Entry(role).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteRoleAsync(int id)
        {
            Role role = await GetRoleByIdAsync(id);

            context.Roles.Remove(role);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();

            return await context.Roles.SingleOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await context.Roles.ToListAsync();
        }

        /// <summary>
        /// Gets a List of Roles
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Role> GetAllByIdsRoles(List<int> ids)
        {
            List<Role> roles = new List<Role>();    

            foreach(int id in ids)
                roles.Add(GetRoleByIdAsync(id).GetAwaiter().GetResult());

            return roles;
        }
    }
}
