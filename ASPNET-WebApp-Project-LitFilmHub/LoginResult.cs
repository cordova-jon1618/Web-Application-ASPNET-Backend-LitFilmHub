using System.IdentityModel.Tokens.Jwt;

namespace ASPNET_WebApp_Project_LitFilmHub
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }

    }
}
