using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository.Users
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
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task SaveUserAsync(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            User user = await GetUserByIdAsync(id, cancellationToken);

            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a User by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            //return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
            return await context.Users.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await context.Users.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets an User by it's name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(name));
        }

        #region Roles



        #endregion

    }
}
