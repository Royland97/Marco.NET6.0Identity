using AutoMapper;
using Core.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserInterface.Web.ViewModels.Authentication;

namespace UserInterface.Web.Controllers
{
    /// <summary>
    /// Authentication Api Controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/login")]
    public class AuthenticationController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Register an User
        /// </summary>
        /// <param name="registerModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody]RegisterModel registerModel,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(registerModel.Email);
            if (user == null)
                return BadRequest("Invalid credentials");

            var token = GenerateToken(registerModel);

            return Ok(new { token });
        }

        /// <summary>
        /// Generates a Token
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        private string GenerateToken(RegisterModel registerModel)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, registerModel.UserName),
                new Claim(ClaimTypes.Email, registerModel.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                    claims: claims,
                                    expires: DateTime.Now.AddHours(24),
                                    signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

    }
}
