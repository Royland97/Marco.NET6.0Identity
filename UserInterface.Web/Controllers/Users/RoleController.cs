using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
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
        private readonly IRoleRepository _roleRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="resourceRepository"></param>
        /// <param name="mapper"></param>
        public RoleController(
            IRoleRepository roleRepository,
            IResourceRepository resourceRepository,
            IMapper mapper)
        {
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

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var role = _mapper.Map<Role>(roleModel);
                /*
                var resources = await _resourceRepository.GetAllResourceByIdsAsync(roleModel.ResourcesIds);

                foreach (var resource in resources)
                    role.Resources.Add(resource);*/

                await _roleRepository.SaveRoleAsync(role, cancellationToken);

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

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var role = _mapper.Map<Role>(roleModel);
                /*
                var resources = await _resourceRepository.GetAllResourceByIdsAsync(roleModel.ResourcesIds);

                role.Resources.Clear();
                foreach (var resource in resources)
                    role.Resources.Add(resource);*/

                await _roleRepository.UpdateRoleAsync(role, cancellationToken);

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
            cancellationToken.ThrowIfCancellationRequested();

            var roleDto = await _roleRepository.GetRoleByIdAsync(id, cancellationToken);

            if (roleDto == null)
                return NotFound(id);

            try
            {
                await _roleRepository.DeleteRoleAsync(id, cancellationToken);

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
        [ProducesResponseType(typeof(List<RoleModelList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await _roleRepository.GetAllRolesAsync(cancellationToken);
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

            var role = await _roleRepository.GetRoleByIdAsync(id, cancellationToken);

            if (role == null)
                return NotFound(id);

            var roleModel = _mapper.Map<RoleModelList>(role);

            return Ok(roleModel);
        }
    }
}
