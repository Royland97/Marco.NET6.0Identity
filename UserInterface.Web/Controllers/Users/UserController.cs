using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.Authorization;
using UserInterface.Web.ViewModels.Users;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// User Api Controller
    /// </summary>
    [Route("/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public UserController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Saves an User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var userExists = await _userManager.FindByEmailAsync(userModel.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status403Forbidden, "The User already exists");

            var user = _mapper.Map<User>(userModel);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result.Succeeded
                ? StatusCode(StatusCodes.Status201Created, userModel)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }

        /// <summary>
        /// Updates an User
        /// </summary>
        /// <param name="userModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<User>(userModel);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded
                ? StatusCode(StatusCodes.Status201Created, userModel)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// 
        /// <param name="id">Id of the User to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(id);

            try
            {
                await _userManager.DeleteAsync(user);
                return Ok(id);
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
        [HttpGet]
        [ProducesResponseType(typeof(List<UserModelList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            var authorize = await _authorizationService.AuthorizeAsync(User, ResourcesNames.GetAllUsers, "ResourceAuthorize");

            if (!authorize.Succeeded)
                return Unauthorized();

            cancellationToken.ThrowIfCancellationRequested();

            var result = await _userRepository.GetAllAsync(cancellationToken);
            var users = _mapper.Map<List<UserModelList>>(result);

            return Ok(users);
        }

        /// <summary>
        /// Gets an User by it's id.
        /// </summary>
        /// 
        /// <param name="id">Id of the User</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(id);

            var userModel = _mapper.Map<UserModel>(user);

            return Ok(userModel);
        }

    }
}
