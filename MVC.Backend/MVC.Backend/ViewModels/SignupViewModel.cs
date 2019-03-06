namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane rejestracji przesyłane z frontendu
    /// </summary>
    public class SignupViewModel
    {
        /// <summary>
        /// Email użytkownika
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Hasło
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Id adresu
        /// </summary>
        public int? AddressId;
    }
}
