using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Users;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// Role Api Controller
    /// </summary>
    [Route("/roles")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        public RoleController(
            RoleManager<Role> roleManager,
            IRoleRepository roleRepository,
            IResourceRepository resourceRepository,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves a Role
        /// </summary>
        /// <param name="roleModel">Role data</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPost]
        [ProducesResponseType(typeof(RoleModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Save(
            [FromBody] RoleModel roleModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var role = _mapper.Map<Role>(roleModel);

            if (!roleModel.ResourcesIds.Any())
            {
                var resources = await _resourceRepository.GetAllByIdsAsync(roleModel.ResourcesIds, cancellationToken);

                foreach (var resource in resources)
                    role.Resources.Add(resource);
            }

            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded
                ? StatusCode(StatusCodes.Status201Created, roleModel)
                : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
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

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var role = _mapper.Map<Role>(roleModel);
                
                var resources = await _resourceRepository.GetAllByIdsAsync(roleModel.ResourcesIds, cancellationToken);

                role.Resources.Clear();
                foreach (var resource in resources)
                    role.Resources.Add(resource);

                await _roleManager.UpdateAsync(role);

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
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound(id);

            try
            {
                await _roleManager.DeleteAsync(role);
                return Ok(id);
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
        [ProducesResponseType(typeof(List<RoleModelList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await _roleRepository.GetAllAsync(cancellationToken);
                var roles = _mapper.Map<List<RoleModelList>>(result);

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
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

            if (role == null)
                return NotFound(id);

            var roleModel = _mapper.Map<RoleModelList>(role);

            return Ok(roleModel);
        }
    }
}
