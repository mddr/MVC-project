using MVC.Backend.Data;
using MVC.Backend.Entities;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace MVC.Backend.Helpers
{
    public static class AuthHelper
    {
        public static byte[] CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            
            return buff;
        }

        public static byte[] CreateHash(string pass, byte[] salt)
        {
            return new HMACSHA512(salt).ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
        }

        public static User Authenticate(string email, string password, UserDbContext context)
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
