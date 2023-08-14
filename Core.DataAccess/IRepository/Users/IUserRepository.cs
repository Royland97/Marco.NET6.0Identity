using Core.Domain.Users;

namespace Core.DataAccess.IRepository.Users
{
    /// <summary>
    /// User Repository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Saves a new User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task SaveUserAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a User by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
