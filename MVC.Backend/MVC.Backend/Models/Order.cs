using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Dane związane z zamówieniem
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public int Id { get; set; }

        /// <summary>
        /// Id użytkownika
        /// </summary>
        [ForeignKey("Users")] public int UserId { get; set; }
        /// <summary>
        /// Użytkownik składający zamówienie
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Id adresu
        /// </summary>
        [ForeignKey("Addresses")] public int? AddressId { get; set; }
        /// <summary>
        /// Adres dostawy
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Id koszyka
        /// </summary>
        [Required] public string CartId { get; set; }
        /// <summary>
        /// Koszyk zawierający zamówione produkty
        /// </summary>
        public List<CartItem> ShoppingCart { get; set; }

        /// <summary>
        /// Cena sumaryczna
        /// </summary>
        [Required] public double TotalPrice { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Required] public DateTime CreatedAt { get; set; }

        public Order()
        {
        }

        public Order(int userId, int? addressId, string cartId, double totalPrice, List<CartItem> cart)
        {
            CreatedAt = DateTime.Now;
            UserId = userId;
            AddressId = addressId;
            CartId = cartId;
            TotalPrice = totalPrice;
            ShoppingCart = cart;
        }
    }
}
