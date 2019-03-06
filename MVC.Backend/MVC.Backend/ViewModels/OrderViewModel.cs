using MVC.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane zamówienia wymieniane między frontem a backendem
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id użytkownika
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id adresu dostawy
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Suma do zapłacenia
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Kupiony koszyk
        /// </summary>
        public List<CartItemViewModel> ShoppingCart { get; set; }

        /// <summary>
        /// Data utworzenia
        /// </summary>
        public DateTime CreatedAt { get; set; }

        public OrderViewModel()
        {
        }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            UserId = order.UserId;
            AddressId = order.AddressId;
            TotalPrice = order.TotalPrice;
            CreatedAt = order.CreatedAt;
            ShoppingCart = order.ShoppingCart.Select(c => new CartItemViewModel(c)).ToList();
        }
        public OrderViewModel(Order order, IEnumerable<CartItem> cart) : this(order)
        {
            ShoppingCart = new List<CartItemViewModel>();
            foreach(var c in cart)
            {
                ShoppingCart.Add(new CartItemViewModel(c));
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var itemViewModel in ShoppingCart)
            {
                sb.Append(itemViewModel + "<br>");
            }
            return sb.ToString();
        }
    }
}