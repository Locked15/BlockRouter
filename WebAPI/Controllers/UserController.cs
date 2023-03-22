using BlockRouter.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BlockRouter.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        #region Private fields.

        private readonly IConfiguration _configuration;

        private readonly List<(string _login, string _password)> _accounts = new()
        {
            ("Admin", "Admin")
        };
        #endregion

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Actions

        [HttpPost]
        [Route("SignIn/{login}/{password}")]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            try
            {
                var user = GetUser(login, password);
                if (user != null)
                {
                    user.Token = GenerateToken();
                    return Ok(user);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User not found.");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error!\n{ex.Message}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Internal Functions.

        private User? GetUser(string login, string password)
        {
            var account = _accounts.Find(user => user._login == login &&
                                                 user._password == password);
            if (!string.IsNullOrEmpty(account._login) && !string.IsNullOrEmpty(account._password))
            {
                return new User()
                {
                    Login = account._login,
                    Password = account._password
                };
            }
            else
            {
                return null;
            }
        }

        private string GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>(Program.JwtKeyName)!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(_configuration.GetValue<string>(Program.JwtIssuerName), _configuration.GetValue<string>(Program.JwtIssuerName),
                                             null, null, DateTime.Now.AddMinutes(30), credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
