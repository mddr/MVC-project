using System.Collections.Generic;
using System.Security.Claims;

namespace MVC.Backend.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        string GenerateConfirmationToken(string email);
        string GenerateResetToken(string email);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
