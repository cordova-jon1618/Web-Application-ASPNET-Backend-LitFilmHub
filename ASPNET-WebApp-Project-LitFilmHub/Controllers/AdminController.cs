﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ClassLibrary_LitFilmHub;
using System.IdentityModel.Tokens.Jwt;
using ASPNET_WebApp_Project_LitFilmHub;

namespace ASPNET_Practice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<LiteratureAndFilmUser> _userManager;
        private readonly JwtHandler _jwtHandler;

        public AdminController(UserManager<LiteratureAndFilmUser> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            LiteratureAndFilmUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Bad user name.");
            }
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
