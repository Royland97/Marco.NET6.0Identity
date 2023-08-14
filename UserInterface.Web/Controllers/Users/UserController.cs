using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Users;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// User Api Controller
    /// </summary>
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="mapper"></param>
        public UserController(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves an User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Bad request, invalid user data</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Save(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var user = _mapper.Map<User>(userModel);

                var roles = await _roleRepository.GetAllRoleByIdsAsync(userModel.RoleIds, cancellationToken);

                foreach (var role in roles)
                    user.Roles.Add(role);

                await _userRepository.SaveUserAsync(user, cancellationToken);

                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">User updated successfully</response>
        /// <response code="400">Bad request, invalid user data</response>
        [HttpPut]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var user = _mapper.Map<User>(userModel);

                var roles = await _roleRepository.GetAllRoleByIdsAsync(userModel.RoleIds, cancellationToken);

                user.Roles.Clear();
                foreach (var role in roles)
                    user.Roles.Add(role);

                await _userRepository.UpdateUserAsync(user, cancellationToken);

                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// 
        /// <param name="id">Id of the User to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// 
        /// <response code="200">User deleted successfully</response>
        /// <response code="404">User not found with the provided Id</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userDto = await _userRepository.GetUserByIdAsync(id, cancellationToken);

            if (userDto == null)
                return NotFound(id);

            try
            {
                await _userRepository.DeleteUserAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">User found with the provided search options</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserModelList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await _userRepository.GetAllUsersAsync(cancellationToken);
                var resources =  _mapper.Map<List<UserModelList>>(result);

                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets an User by it's id.
        /// </summary>
        /// 
        /// <param name="id">Id of the User</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// 
        /// <response code="200">User data</response>
        /// <response code="404">User not found with the provided ID</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);

            if (user == null)
                return NotFound(id);

            var userModel = _mapper.Map<UserModel>(user);

            return Ok(userModel);
        }

    }
}
