using AutoMapper;
using Core.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.ViewModels.Authentication;

namespace UserInterface.Web.Controllers
{
    /// <summary>
    /// Authentication Api Controller
    /// </summary>
    [Route("/authentication")]
    [ApiController]
    public class AuthenticationController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private IMapper _mapper;

        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Register an User
        /// </summary>
        /// <param name="registerModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpPost]
        public async Task<IActionResult> Registrer(
            [FromBody]RegisterModel registerModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status403Forbidden, "The User already exists");

                var identityUser = _mapper.Map<User>(registerModel);
                identityUser.SecurityStamp = Guid.NewGuid().ToString();

                var result = await _userManager.CreateAsync(identityUser, registerModel.Password);

                return result.Succeeded
                    ? StatusCode(StatusCodes.Status201Created, registerModel)
                    : StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            
        }

    }
}
