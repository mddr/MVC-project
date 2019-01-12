using MVC.Backend.Models;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za obsługe emailów
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Wysyła email potwierdzający na podany adres emailowy
        /// </summary>
        /// <param name="address">Adres na który wysłać emial</param>
        void SendConfirmationEmail(string address);
        /// <summary>
        /// Wysyła email z linkiem do resetu hasła na podany adres
        /// </summary>
        /// <param name="address">Adresn na który wysłać email</param>
        void SendPasswordReset(string address);
        /// <summary>
        /// Wysyła email z informacją o złożonym zamówieniu
        /// </summary>
        /// <param name="address">Adresn na który wysłać email</param>
        /// <param name="order">Dane zamówienia</param>
        void SendOrderInfo(string address, Order order);
        /// <summary>
        /// Rozsyła newstlettery do użytkowników akceptujących je
        /// </summary>
        void SendNewsletter();
    }
}
