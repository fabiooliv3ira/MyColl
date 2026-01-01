using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using RESTfulAPIMYCOLL.Data;
using RESTfulAPIMYCOLL.Entities.Dto;

namespace RESTfulAPIMYCOLL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth2Controller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        public Auth2Controller(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDto loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _userManager.FindByEmailAsync(loginData.Email).Result;
            if (user == null) 
                return BadRequest(ModelState);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);
            if (!result.Succeeded) 
                return BadRequest(ModelState);
            var token = GenerateJwtToken(user);
            
            return Ok(
                new AuthResponseDto
                {
                    AccessToken = token.Result,
                    JsonTokenType = "Bearer",
                    ExpiresIn = 3600,
                    EmailTokenProvider = user.Email ?? string.Empty
                });
        }
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {      
                UserName = registerData.UserName,
                Email = registerData.Email,
                Nome = registerData.Nome,
                NIF = registerData.NIF,
                DataRegisto = DateTime.UtcNow,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, registerData.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user, registerData.TipoUtilizador);
            return Ok();
        }
        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtKey = _config["JWT:Key"] ?? throw new InvalidOperationException();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email??string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email??string.Empty)
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}
