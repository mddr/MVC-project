using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Helpers;
using MVC.Backend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _usersDb;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountController(ApplicationDbContext usersDb, ITokenService tokenService, IEmailService emailService)
        {
            _usersDb = usersDb;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupViewModel viewModel)
        {
            var user = _usersDb.Users.SingleOrDefault(u => u.Email == viewModel.Email);
            if (user != null) return Conflict();

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(viewModel.Password, salt);
            var newUser = new User(viewModel.Email, passwordHash, salt, viewModel.FirstName, viewModel.LastName, viewModel.Address);
            _usersDb.Users.Add(newUser);
            await _usersDb.SaveChangesAsync();

            var host = Request.Host;
            _emailService.SendConfirmationEmail(viewModel.Email, host.ToString());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SignupAdmin([FromBody] SignupViewModel viewModel)
        {
            var user = _usersDb.Users.SingleOrDefault(u => u.Email == viewModel.Email);
            if (user != null) return Conflict();

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(viewModel.Password, salt);
            var newUser = new User(viewModel.Email, passwordHash, salt, viewModel.FirstName, viewModel.LastName, viewModel.Address, role:Enums.Roles.Admin);
            _usersDb.Users.Add(newUser);

            await _usersDb.SaveChangesAsync();

            var host = Request.Host;
            _emailService.SendConfirmationEmail(viewModel.Email, host.ToString());

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = AuthHelper.Authenticate(loginViewModel.Email, loginViewModel.Password, _usersDb);
            if (user == null) return BadRequest();

            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var jwtToken = _tokenService.GenerateAccessToken(userClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _usersDb.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken
            });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var claims = _tokenService.GetPrincipalFromExpiredToken(token);
            var email = claims.Claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();
            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                return BadRequest();

            user.EmailConfirmed = true;
            await _usersDb.SaveChangesAsync();

            return Ok();
        }
    }
}
