using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Users")] public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Addresses")] public int? AddressId { get; set; }
        public Address Address { get; set; }

        [Required] public string CartId { get; set; }
        public List<CartItem> ShoppingCart { get; set; }

        [Required] public double TotalPrice { get; set; }
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
