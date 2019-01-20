using System.Collections.Generic;
using System.Security.Claims;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za operacje związane z JWT
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generuje token dostepu
        /// </summary>
        /// <param name="claims">Informacje o użytkowniku</param>
        /// <returns>Token dostępu</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);
        /// <summary>
        /// Generuje token do odświeżania
        /// </summary>
        /// <returns>Token do odświeżania</returns>
        string GenerateRefreshToken();
        /// <summary>
        /// Generuje token potwierdzający mail
        /// </summary>
        /// <param name="email">Email uzytkownika</param>
        /// <returns>Token potwierdzający mail</returns>
        string GenerateConfirmationToken(string email);
        /// <summary>
        /// Generuje token resetu hasła
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <returns>Token resetu hasla</returns>
        string GenerateResetToken(string email);
        /// <summary>
        /// Wyciąga informacje o użytkowniku z tokenu
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="validateLifetime">Flaga odpowiadająca za walidowanie czasu życia tokenu lub nie</param>
        /// <returns>Informacje o użytkowniku</returns>
        ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime);
    }
}
