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
            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user != null) return StatusCode(409);

            byte[] salt = AuthHelper.CreateSalt(128);
            _usersDb.Users.Add(new User
            {
                Id = _usersDb.Users.Max(x => x.Id) + 1,
                Email = email,
                PasswordHash = AuthHelper.CreateHash(password, salt),
                PasswordSalt  = salt
            });


            await _usersDb.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = AuthHelper.Authenticate(email, password, _usersDb);

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
                refreshToken = refreshToken
            });
        }

        [Authorize]
        public async Task<IActionResult> test()
        {
            return Ok("asdasd");
        }
    }
}
