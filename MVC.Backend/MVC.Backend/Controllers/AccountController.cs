using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Entities;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserDbContext _usersDb;
        private readonly ITokenService _tokenService;

        public AccountController(UserDbContext usersDb, ITokenService tokenService)
        {
            _usersDb = usersDb;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(string email, string password)
        {
            //todo: SignupViewModel
            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user != null) return Conflict();

            var salt = AuthHelper.CreateSalt(128);
            var newUser = new User(email, AuthHelper.CreateHash(password, salt), salt);
            _usersDb.Users.Add(newUser);

            await _usersDb.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = AuthHelper.Authenticate(loginViewModel.Email, loginViewModel.Password, _usersDb);
            if (user == null) return BadRequest();

            var usersClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Email)
            };

            var jwtToken = _tokenService.GenerateAccessToken(usersClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _usersDb.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken
            });
        }
    }
}
