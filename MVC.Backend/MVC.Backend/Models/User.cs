using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC.Backend.Helpers;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Klasa opisująca użytkownika
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public int Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required] public string Email { get; set; }
        /// <summary>
        /// Hash hasla
        /// </summary>
        [Required] public byte[] PasswordHash { get; set; }
        /// <summary>
        /// Sól do hasła
        /// </summary>
        [Required] public byte[] PasswordSalt { get; set; }
        /// <summary>
        /// JWT odwierzający
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Rola użytkownika
        /// </summary>
        [Required] public Enums.Roles Role { get; set; }
        /// <summary>
        /// Czy potwierdzono mail
        /// </summary>
        [Required] public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        [Required] public string FirstName { get; set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        [Required] public string LastName { get; set; }
        /// <summary>
        /// Id Adresu
        /// </summary>
        [ForeignKey("Address")] public int? AddressId { get; set; }
        /// <summary>
        /// Adres zamieszkania
        /// </summary>
        public Address Address;

        /// <summary>
        /// Preferowana waluta
        /// </summary>
        [Required] public Enums.Currency Currency { get; set; }
        /// <summary>
        /// Czy preferuje ceny netto
        /// </summary>
        public bool PrefersNetPrice { get; set; }
        /// <summary>
        /// Czy akceptuje newsletter
        /// </summary>
        public bool AcceptsNewsletters { get; set; }
        /// <summary>
        /// Preferowana liczba produktów na strone
        /// </summary>
        public int ProductsPerPage { get; set; }

        /// <summary>
        /// Id koszyka
        /// </summary>
        [ForeignKey("CartItems")] public string CartId { get; set; }
        /// <summary>
        /// Koszyk
        /// </summary>
        public List<CartItem> ShoppingCart { get; set; }

        public User()
        {
        }

        public User(string email, byte[] passwordHash, byte[] passwordSalt,
            string firstName, string lastName, int? addressId,
            bool prefersNetPrice = false, int productsPerPage = 10,
            Enums.Roles role = Enums.Roles.User, Enums.Currency currency = Enums.Currency.PLN,
            bool acceptsNewsletters = true)
        {
            AcceptsNewsletters = acceptsNewsletters;
            AddressId = addressId;
            Currency = currency;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            PrefersNetPrice = prefersNetPrice;
            ProductsPerPage = productsPerPage;
            Role = role;
            EmailConfirmed = false;
        }
    }
}
