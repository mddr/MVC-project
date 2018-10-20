using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _usersDb;
        public TokenController(ITokenService tokenService, ApplicationDbContext usersDb)
        {
            _tokenService = tokenService;
            _usersDb = usersDb;
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] TokenViewModel tokenViewModel)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenViewModel.AccessToken);
            var email = principal.Identity.Name; 

            var user = _usersDb.Users.SingleOrDefault(u => u.Email == email);
            if (user == null || user.RefreshToken != tokenViewModel.RefreshToken) return BadRequest();

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

            return Ok();
        }

    }
}
