using Core.Domain.Users;
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
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(registerModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, registerModel.Password))
            {
                var token = GenerateToken(registerModel);

                return Ok(new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized("Invalid credentials");
        }

        /// <summary>
        /// Generates a Token
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        private JwtSecurityToken GenerateToken(RegisterModel registerModel)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, registerModel.UserName),
                new Claim(ClaimTypes.Email, registerModel.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds
            );

            return securityToken;
        }

    }
}
