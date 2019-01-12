using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Services;
using MVC.Backend.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Controllers
{
    /// <summary>
    /// Zajmuje sie obslugą JWT
    /// </summary>
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _usersDb;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="tokenService">Instancja klasy tworzona przez DI implementująca interfejs</param>
        /// <param name="usersDb">Kontekst bazodanowyu</param>
        public TokenController(ITokenService tokenService, ApplicationDbContext usersDb)
        {
            _tokenService = tokenService;
            _usersDb = usersDb;
        }

        /// <summary>
        /// Odświerza otrzymany token
        /// </summary>
        /// <param name="tokenViewModel">Token użyutkownika</param>
        /// <returns>Nowy token o przedłużonej ważności</returns>
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

        /// <summary>
        /// Unieważnia token użytkownika
        /// </summary>
        /// <returns>Ok</returns>
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
