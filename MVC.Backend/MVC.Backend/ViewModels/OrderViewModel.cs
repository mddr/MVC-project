using System;
using System.Collections.Generic;
using MVC.Backend.Models;
using System.Linq;
using System.Text;

namespace MVC.Backend.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? AddressId { get; set; }

        public double TotalPrice { get; set; }

        public List<CartItemViewModel> ShoppingCart { get; set; }

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