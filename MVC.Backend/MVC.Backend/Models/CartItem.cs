using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Backend.Models
{
    public class CartItem
    {
        public string Id { get; set; }

        [ForeignKey("Users")] public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Products")] public string ProductId { get; set; }
        public Product Product { get; set; }

        [Required] public int ProductAmount { get; set; }
        [Required] public bool IsValid { get; set; }

        public CartItem()
        {
            Id = Guid.NewGuid().ToString();
        }

        public CartItem(string cartId, string productId, int productAmount, int userId, bool isValid = true)
        {
            Id = cartId;
            ProductId = productId;
            ProductAmount = productAmount;
            UserId = userId;
            IsValid = isValid;
        }
    }
}