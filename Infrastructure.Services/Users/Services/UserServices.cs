using AutoMapper;
using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.Services.Users.Models;
using Infrastructure.Services.Users.IServices;

namespace Infrastructure.Services.Users.Services
{
    /// <summary>
    /// User Services
    /// </summary>
    public class UserServices: IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserServices"/> class.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves a User
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveUserAsync(UserModel userModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<User>(userModel);
            await _userRepository.SaveUserAsync(user);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateUserAsync(UserModel userModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<User>(userModel);
            await _userRepository.UpdateUserAsync(user);
        }

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _userRepository.DeleteUserAsync(id);
        }

        /// <summary>
        /// Gets All Users
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<UserModelList>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserModelList>>(result);
        }

        /// <summary>
        /// Gets an User by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            User user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserModel>(user);
        }

    }
}
