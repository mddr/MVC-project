namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane logowania wymieniane między frontem a backendem
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Hasło
        /// </summary>
        public string Password { get; set; }
    }
}
