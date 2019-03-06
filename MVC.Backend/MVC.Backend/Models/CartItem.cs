using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Przedmiot koszyka
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Id użytkownika
        /// </summary>
        [ForeignKey("Users")] public int UserId { get; set; }
        /// <summary>
        /// Przedmiot w koszyku użytkownika
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Id zamówienia
        /// </summary>
        [ForeignKey("Orders")] public int? OrderId { get; set; }
        /// <summary>
        /// Id produktu
        /// </summary>
        [ForeignKey("Products")] public string ProductId { get; set; }
        /// <summary>
        /// Dane zamówionego produktu
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Ilość zamówionego produktu
        /// </summary>
        [Required] public int ProductAmount { get; set; }
        /// <summary>
        /// Czy zamówienie jest poprawne
        /// </summary>
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