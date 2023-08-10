using Infrastructure.Services.Users.Models;

namespace Infrastructure.Services.Users.IServices
{
    /// <summary>
    /// User Services
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// Saves a User
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveUserAsync(UserModel userModel, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateUserAsync(UserModel userModel, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteUserAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets All Users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<UserModelList>> GetAllUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets an User by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserModel> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
