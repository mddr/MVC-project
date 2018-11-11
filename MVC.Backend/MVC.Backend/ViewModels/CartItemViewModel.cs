using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public int ProductAmount { get; set; }
        public bool IsValid { get; set; }

        public CartItemViewModel()
        {
        }

        public CartItemViewModel(CartItem cartItem)
        {
            ProductAmount = cartItem.ProductAmount;
            ProductId = cartItem.ProductId;
            IsValid = cartItem.IsValid;
        }

        public static List<CartItemViewModel> ToList(List<CartItem> cartItems)
        {
            return cartItems?.Select(cartItem => new CartItemViewModel(cartItem)).ToList();
        }
    }
}
