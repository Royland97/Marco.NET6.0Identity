using AutoMapper;
using Core.DataAccess.IRepository.Users;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Users;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// Resource Api Controller
    /// </summary>.
    [Route("/resources")]
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceController"/> class.
        /// </summary>
        /// <param name="resourceRepository"></param>
        /// <param name="mapper"></param>
        public ResourceController(IResourceRepository resourceRepository, IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">Resource found with the provided search options</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ResourceModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var result = await _resourceRepository.GetAllAsync(cancellationToken);
                var resources = _mapper.Map<List<ResourceModel>>(result);

                return Ok(resources);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
