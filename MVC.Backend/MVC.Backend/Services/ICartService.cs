using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface ICartService
    {
        List<CartItem> GetCartItems(int userId);
        string AddToCart(CartItemViewModel viewModel, int userId); //returns CartId
        void UpdateCart(CartItemViewModel viewModel, int userId);
        void RemoveCart(int userId);
        void RemoveFromCart(string productId, int userId);
    }
}
