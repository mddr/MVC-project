namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Umożliwia pobranie ustawień dotycząchych usługi mailowej z appsettings.json
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Host usługi 
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Port usługi
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Konto email systemu
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Hasło do konta systemu
        /// </summary>
        public string Password { get; set; }
    }
}
