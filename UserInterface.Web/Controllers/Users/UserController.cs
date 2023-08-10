using Infrastructure.Services.Users.Models;
using Infrastructure.Services.Users.IServices;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// User Api Controller
    /// </summary>
    [Route("/users")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userServices"></param>
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
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

            try
            {
                userModel.Id = await _userServices.SaveUserAsync(userModel, cancellationToken);
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

            try
            {
                await _userServices.UpdateUserAsync(userModel, cancellationToken);
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
        /// <response code="200">User delete successfully</response>
        /// <response code="404">User not found with the provided Id</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var userDto = await _userServices.GetUserByIdAsync(id, cancellationToken);

            if (userDto == null)
                return NotFound(id);

            try
            {
                await _userServices.DeleteUserAsync(id, cancellationToken);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var users = await _userServices.GetAllUsersAsync(cancellationToken);
                return Ok(users);

            }catch (Exception ex)
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
            var userDto = await _userServices.GetUserByIdAsync(id, cancellationToken);

            if (userDto == null)
                return NotFound(id);

            return Ok(userDto);
        }

    }
}
