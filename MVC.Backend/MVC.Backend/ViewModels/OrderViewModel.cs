using System;
using System.Collections.Generic;
using MVC.Backend.Models;
using System;
using System.Linq;

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
            ShoppingCart = CartItemViewModel.ToList(order.ShoppingCart);
        }
        public OrderViewModel(Order order, List<CartItem> cart) : this(order)
        {
            ShoppingCart = new List<CartItemViewModel>();
            //ShoppingCart = cart.Select(c => new CartItemViewModel(c)).ToList();
            foreach(var c in cart)
            {
                ShoppingCart.Add(new CartItemViewModel(c));
            }
        }

        public static List<OrderViewModel> ToList(List<Order> orders)
        {
            var viewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                viewModels.Add(new OrderViewModel(order));
            }

            return viewModels;
        }
    }
}