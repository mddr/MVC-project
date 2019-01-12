using MVC.Backend.Data;
using System;
using System.Linq;
using System.Security.Cryptography;
using MVC.Backend.Models;

namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Klasa pomocnicza do uwierzytelniania użytkowników
    /// </summary>
    public static class AuthHelper
    {
        /// <summary>
        /// Tworzy sól o podanym rozmiarze
        /// </summary>
        /// <param name="size">Rozmiar w liczbie bajtów</param>
        /// <returns>Sól w postaci bajtów</returns>
        public static byte[] CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            
            return buff;
        }

        /// <summary>
        /// Tworzy hash hasła z użyciem soli
        /// </summary>
        /// <param name="pass">Hasło podane przez użytkownika</param>
        /// <param name="salt">Wygenerowana sól</param>
        /// <returns>Hash w postaci bajtów</returns>
        public static byte[] CreateHash(string pass, byte[] salt)
        {
            return new HMACSHA512(salt).ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
        }

        /// <summary>
        /// Uwieżytelnia użytkownika jeżeli hash stworzony z podanego hasła zgadza się z tym w bazie
        /// </summary>
        /// <param name="email">Email podany przez użytkownika</param>
        /// <param name="password">Hasło podane przez użytkownika</param>
        /// <param name="context"></param>
        /// <returns>Dane użytkownika lub null</returns>
        public static User Authenticate(string email, string password, ApplicationDbContext context)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = context.Users.SingleOrDefault(x => x.Email == email);

            if (user == null)
                return null;

            if (!CreateHash(password, user.PasswordSalt).SequenceEqual(user.PasswordHash))
                return null;
            
            return user;
        }

    }
}
