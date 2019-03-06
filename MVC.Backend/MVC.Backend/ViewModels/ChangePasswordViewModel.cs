namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane zmiany hasła przesyłane z frontu
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Stare hasło do walidacji
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// Nowe hasło
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// Dane przekazywane z frontu przy resecie hasła
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Email uzytkownika
        /// </summary>
		public string Email { get; set; }
        /// <summary>
        /// Nowe hasło
        /// </summary>
		public string NewPassword { get; set; }
        /// <summary>
        /// Token do walidacji
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// Email użytkownika który zapomniał hasła
    /// </summary>
	public class RequestResetPasswordViewModel
	{
        /// <summary>
        /// Email
        /// </summary>
		public string Email { get; set; }
	}
}
