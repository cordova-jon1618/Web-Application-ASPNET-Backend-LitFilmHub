using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ClassLibrary_LitFilmHub;
using System.IdentityModel.Tokens.Jwt;
using ASPNET_WebApp_Project_LitFilmHub;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<LiteratureAndFilmUser> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ILogger<AdminController> _logger;

        public AdminController(UserManager<LiteratureAndFilmUser> userManager, JwtHandler jwtHandler, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _logger = logger; 

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            _logger.LogInformation($"Attempting login for user: {loginRequest.UserName}"); // Log the username

            LiteratureAndFilmUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Bad user name.");
            }

            _logger.LogInformation($"Checking password for user: {loginRequest.UserName}"); // Log before checking password

            bool success = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!success)
            {
                return Unauthorized("Bad password.");
            }

            JwtSecurityToken secToken = await _jwtHandler.GetTokenAsync(user);
            string? jwtstr = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Mom loves me",
                Token = jwtstr
            });

        }
    }
}
