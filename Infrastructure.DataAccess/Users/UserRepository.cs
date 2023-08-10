using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Users
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Saves a new User
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveUserAsync (User user)
        {
            if(user == null)
                throw new ArgumentNullException();

            user.UserGuid = Guid.NewGuid();

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateUserAsync (User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteUserAsync(int id)
        {
            User user = await GetUserByIdAsync(id);

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a User by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync (int id)
        {
            if(id < 0)
                throw new ArgumentNullException();

            return await context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync ()
        {
            return await context.Users.ToListAsync();
        }

    }
}
