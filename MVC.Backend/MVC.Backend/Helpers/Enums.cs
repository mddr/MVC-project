namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Typy wyliczeniowe
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Rodzaje walut
        /// </summary>
        public enum Currency
        {
            PLN = 0,
            EUR = 1,
            USD = 2
        }

        /// <summary>
        /// Role użytkowników
        /// </summary>
        public enum Roles
        {
            User = 0,
            Admin = 1
        }
    }
}
