using Infrastructure.Services.Users.IServices;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.Controllers.Users
{
    /// <summary>
    /// Resource Api Controller
    /// </summary>.
    [Route("/resources")]
    public class ResourceController : Controller
    {
        private readonly IResourceServices _resourceServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceController"/> class.
        /// </summary>
        /// <param name="resourceServices"></param>
        public ResourceController(IResourceServices resourceServices)
        {
            _resourceServices = resourceServices;
        }

        /// <summary>
        /// Gets all Resources
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken" /> used to propagate notifications that the operation should be canceled.
        /// </param>
        /// <response code="200">Resource found with the provided search options</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resources = await _resourceServices.GetAllResourcesAsync(cancellationToken);
                return Ok(resources);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
