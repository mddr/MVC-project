namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Tokeny JTW przesyłane na frontend
    /// </summary>
    public class TokenViewModel
    {
        /// <summary>
        /// Token dostepu
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Token odświeżania
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
