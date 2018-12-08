using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public int ProductAmount { get; set; }
        public bool IsValid { get; set; }
        public ProductViewModel Product { get; set; }

        public CartItemViewModel()
        {
        }

        public CartItemViewModel(CartItem cartItem)
        {
            ProductAmount = cartItem.ProductAmount;
            ProductId = cartItem.ProductId;
            Product = new ProductViewModel(cartItem.Product);
            IsValid = cartItem.IsValid;
        }

        public CartItemViewModel(CartItem cartItem, Product product) : this(cartItem)
        {
            Product = new ProductViewModel(product);
        }

        public static List<CartItemViewModel> ToList(List<CartItem> cartItems)
        {
            return cartItems?.Select(cartItem => new CartItemViewModel(cartItem)).ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Product);
            sb.Append($"Ilość: {ProductAmount}<br>");
            sb.Append("<hr>");

            return sb.ToString();
        }
    }
}
