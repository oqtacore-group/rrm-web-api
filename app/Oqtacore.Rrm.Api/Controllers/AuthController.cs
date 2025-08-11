using Oqtacore.Rrm.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Oqtacore.Rrm.Application.Commands.Users;
using Oqtacore.Rrm.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly ApplicationContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        public AuthController(UserManager<AspNetUser> userManager, SignInManager<AspNetUser> signInManager, ApplicationContext dataContext, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            var checkResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (checkResult.IsLockedOut)
                return Unauthorized(new { message = "User account is locked out." });

            if (checkResult.IsNotAllowed)
                return Unauthorized(new { message = "User is not allowed to sign in." });

            if (checkResult.RequiresTwoFactor)
                return Unauthorized(new { message = "Two-factor authentication is required." });

            if (!checkResult.Succeeded)
                return Unauthorized(new { message = "Invalid username or password." });

            var token = GenerateJwtToken(user);
            var result = new LoginUserResult { Id = user.Id, UserName = user.UserName, Email = user.Email, Token = token };

            _logger.LogInformation("User logged in successfully.");

            return Ok(result);
        }

        [HttpGet("IsAuthenticated")]
        [Authorize]
        public ActionResult<bool> IsAuthenticated()
        {            
            return Ok(true);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody]AddUserCommand request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                //return Ok(new { success = false, message = "User email is already exists." });

                user = new AspNetUser { UserName = request.Email, Email = request.Email };

                var result = await _userManager.CreateAsync(user, request.Password);
            }

            var admin = await _dataContext.Admin.FirstOrDefaultAsync(x => x.Email == user.Email);
            if(admin == null)
            {
                admin = new Domain.Models.Admin { AuthId = user.Id, Email = user.Email, Name = request.Name, Description = request.Name };

                _dataContext.Add(admin);
                await _dataContext.SaveChangesAsync();
            }

            return Ok(new { success = true, user = new { admin.id, admin.AuthId, user.Email }, message = "User created successfully." });
        }
        private string GenerateJwtToken(AspNetUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BZJwFx1ZC/+NdR6XNwK1CAuwAZ4fO1vZBHpR8FFAFzA="));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}