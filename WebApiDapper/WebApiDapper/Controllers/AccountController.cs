using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WebAPICoreDapper.Models;
using WebApiDapper.ActionFilters;
using WebApiDapper.DTOs.Login;

namespace WebApiDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IConfiguration configuration,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            var user = await _userManager.FindByNameAsync(loginData.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginData.UserName, loginData.Password, false, true);
                if (!result.Succeeded) return BadRequest("Mật khẩu không đúng");
                var roleUser = _userManager.GetRolesAsync(user);
                //var claim = new[]
                //{
                //    //new Claim("Email", user.Email),
                //    //new Claim(ClaimTypes.NameIdentifier, user.UserName),
                //    //new Claim(SystemConstants.UserClaim.Id, user.Id.ToString()),
                //    //new Claim(ClaimTypes.Name, user.UserName),
                //    //new Claim(SystemConstants.UserClaim.FullName, user.FullName),
                //    //new Claim(SystemConstants.UserClaim.Avatar, string.IsNullOrEmpty(user.Avatar) ? string.Empty : user.Avatar),
                //    //new Claim(SystemConstants.UserClaim.Roles, string.Join(";", roles)),
                //    //new Claim(SystemConstants.UserClaim.Permissions, JsonConvert.SerializeObject(permissions)),
                //    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //};
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                     //claim,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return NotFound("Không tìm thấy tài khoản");
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Register(RegisterModel registerData)
        {
            var user = new AppUser { FullName = registerData.FullName, UserName = registerData.Email, Email = registerData.Email };

            var result = await _userManager.CreateAsync(user, registerData.Password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                //await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));

                //await _signInManager.SignInAsync(user, false);

                return Ok(result.Succeeded);
            }

            return BadRequest();
        }
    }
}
