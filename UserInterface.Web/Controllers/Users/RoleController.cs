using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Core.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<IActionResult> Save(
            [FromBody] RoleModel roleModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var roleExists = await _roleManager.FindByIdAsync(roleModel.Id);
            if(roleExists != null)
                return StatusCode(StatusCodes.Status403Forbidden, "The Role already exists");

            var role = _mapper.Map<Role>(roleModel);

            var result = await _roleManager.CreateAsync(role);
            await _roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Name, role.Name));

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
        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] RoleModel roleModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var role = await _roleManager.FindByIdAsync(roleModel.Id);

                if(role == null)
                    return NotFound(roleModel.Id);

                role = _mapper.Map(roleModel, role);
                var result = await _roleManager.UpdateAsync(role);

                return result.Succeeded
                    ? StatusCode(StatusCodes.Status201Created, roleModel)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
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
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpDelete("{id}")]
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
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded
                    ? StatusCode(StatusCodes.Status200OK, id)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
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
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _roleRepository.GetAllAsync(cancellationToken);
            var roles = _mapper.Map<ICollection<RoleModelList>>(result);

            return Ok(roles);
        }

        /// <summary>
        /// Gets a Role by Id.
        /// </summary>
        /// 
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound(id);

            var roleModel = _mapper.Map<RoleModelList>(role);

            return Ok(roleModel);
        }

        #region Resources

        /// <summary>
        /// Gets all Role Resources by Id
        /// </summary>
        /// 
        /// <param name="id">Role Id</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpGet("{id}/resources")]
        public async Task<IActionResult> GetResourcesByRoleId(
            [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound(id);

            return Ok(role.Resources?.ToList());
        }

        /// <summary>
        /// Add a Resources List to Role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="resourcesIds">Resources List</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        [HttpPut("{id}/resources")]
        public async Task<IActionResult> AddResourcesToRole(
            [FromRoute] string id,
            [FromBody] List<string> resourcesIds,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return NotFound(id);

            if (resourcesIds.Any())
            {
                var resources = await _resourceRepository.GetAllByIdsAsync(resourcesIds, cancellationToken);

                role.Resources.Clear();
                foreach (var resource in resources)
                    role.Resources.Add(resource);

                var result = await _roleManager.UpdateAsync(role);

                return result.Succeeded
                    ? StatusCode(StatusCodes.Status201Created, resourcesIds)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            else
            {
                return BadRequest("The Resource List contains any elements");
            }
        }

        #endregion
    }
}
