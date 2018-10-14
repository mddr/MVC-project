using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserDbContext _usersDb;
        public TokenController(ITokenService tokenService, UserDbContext usersDb)
        {
            _tokenService = tokenService;
            _usersDb = usersDb;
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(string token, string refreshToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(token);
            var email = principal.Identity.Name; 

            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user == null || user.RefreshToken != refreshToken) return BadRequest();

            var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _usersDb.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Revoke()
        {
            var email = User.Identity.Name;

            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return BadRequest();

            user.RefreshToken = null;

            await _usersDb.SaveChangesAsync();

            return NoContent();
        }

    }
}
