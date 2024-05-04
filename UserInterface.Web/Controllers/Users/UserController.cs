using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserInterface.Web.ViewModels.Authentication;
using UserInterface.Web.ViewModels.Users;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// User Api Controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public UserController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IMapper mapper,
            IRoleRepository roleRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Saves an User
        /// </summary>
        /// <param name="registerModel">User data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(
            [FromBody] RegisterModel registerModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status403Forbidden, "The User already exists");

            var user = _mapper.Map<User>(registerModel);

            user.Active = true;

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

            return result.Succeeded
                ? StatusCode(StatusCodes.Status201Created, registerModel)
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
        public async Task<IActionResult> Update(
            [FromBody] UserModel userModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var user = await _userManager.FindByIdAsync(userModel.Id);

                if (user == null)
                    return NotFound(userModel.Id);

                user = _mapper.Map(userModel, user);
                var result = await _userManager.UpdateAsync(user);

                return result.Succeeded
                    ? StatusCode(StatusCodes.Status201Created, userModel)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// 
        /// <param name="id">User Id to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
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
        [Authorize(Policy = "ActivePolicy")]
        [HttpGet]
        public IActionResult GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var result = _userManager.Users.ToList();
            var users = _mapper.Map<ICollection<UserModelList>>(result);

            return Ok(users);
        }

        /// <summary>
        /// Gets an User by Id.
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
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

        #region Roles

        /// <summary>
        /// Gets all User Roles by Id
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesByUserId(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(id);

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        /// <summary>
        /// Add a Role List to User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="rolesIds">Role List to add</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/roles")]
        public async Task<IActionResult> AddRolesToUser(
            [FromRoute] string id,
            [FromBody] List<string> rolesIds,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(id);

            if (rolesIds.Any())
            {
                var roles = await _roleRepository.GetAllByIdsAsync(rolesIds, cancellationToken);

                try
                {
                    foreach (var role in roles)
                        await _userManager.AddToRoleAsync(user, role.Name);

                    return Ok(rolesIds);
                }catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("The Role List contains any elements");
            }
        }

        /// <summary>
        /// Deletes an User Role by Name
        /// </summary>
        /// 
        /// <param name="id">User Id</param>
        /// <param name="roleName">Role Name</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/roles/{roleName}")]
        public async Task<IActionResult> DeleteRolesFromUser(
            [FromRoute] string id,
            [FromRoute] string roleName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(id);

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
                return NotFound(roleName);

            try
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
                return Ok(roleName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}
