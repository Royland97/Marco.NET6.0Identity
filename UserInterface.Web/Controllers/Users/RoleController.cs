using Infrastructure.Services.Users.IServices;
using Infrastructure.Services.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// Role Api Controller
    /// </summary>
    [Route("/roles")]
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="roleServices"></param>
        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        /// <summary>
        /// Saves a Role
        /// </summary>
        /// <param name="roleModel">Role data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="201">Role created successfully</response>
        /// <response code="400">Bad request, invalid role data</response>
        [HttpPost]
        [ProducesResponseType(typeof(RoleModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Save(
            [FromBody] RoleModel roleModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _roleServices.SaveRoleAsync(roleModel, cancellationToken);
                return Ok(roleModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="roleModel">Role data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">Role updated successfully</response>
        /// <response code="400">Bad request, invalid role data</response>
        [HttpPut]
        [ProducesResponseType(typeof(RoleModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(
            [FromBody] RoleModel roleModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _roleServices.UpdateRoleAsync(roleModel, cancellationToken);
                return Ok(roleModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// 
        /// <param name="id">Id of the Role to delete</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// 
        /// <response code="200">Role deleted successfully</response>
        /// <response code="404">Role not found with the provided Id</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var roleDto = await _roleServices.GetRoleByIdAsync(id, cancellationToken);

            if (roleDto == null)
                return NotFound(id);

            try
            {
                await _roleServices.DeleteRoleAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">Role found with the provided search options</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var roles = await _roleServices.GetAllRolesAsync(cancellationToken);
                return Ok(roles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets a Role by it's id.
        /// </summary>
        /// 
        /// <param name="id">Id of the Role</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// 
        /// <response code="200">Role data</response>
        /// <response code="404">Role not found with the provided ID</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RoleModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var roleDto = await _roleServices.GetRoleByIdAsync(id, cancellationToken);

            if (roleDto == null)
                return NotFound(id);

            return Ok(roleDto);
        }
    }
}
