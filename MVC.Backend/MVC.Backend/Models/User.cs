using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC.Backend.Helpers;

namespace MVC.Backend.Models
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required] public string Email { get; set; }
        [Required] public byte[] PasswordHash { get; set; }
        [Required] public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }
        [Required] public Enums.Roles Role { get; set; }
        [Required] public bool EmailConfirmed { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public Address Address;

        [Required] public Enums.Currency Currency { get; set; }
        public bool PrefersNetPrice { get; set; }
        public bool AcceptsNewsletters { get; set; }
        public int ProductsPerPage { get; set; }

        [ForeignKey("CartItems")] public string CartId { get; set; }
        public List<CartItem> ShoppingCart { get; set; }

        public User()
        {
        }

        public User(string email, byte[] passwordHash, byte[] passwordSalt,
            string firstName, string lastName, Address address,
            bool prefersNetPrice = false, int productsPerPage = 10,
            Enums.Roles role = Enums.Roles.User, Enums.Currency currency = Enums.Currency.PLN,
            bool acceptsNewsletters = true)
        {
            AcceptsNewsletters = acceptsNewsletters;
            Address = address;
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
