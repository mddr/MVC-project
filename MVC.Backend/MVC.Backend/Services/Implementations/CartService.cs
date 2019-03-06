using Microsoft.EntityFrameworkCore;
using MVC.Backend.Data;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Backend.Services
{
    /// <see cref="ICartService"/>
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <see cref="ICartService.GetCartItems(int)"/>
        /// <exception cref="ArgumentException"/>
        public List<CartItem> GetCartItems(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");

            var cartId = user.CartId;
            var cart = _context.CartItems
						.Include("Product")
						.Where(i => i.Id == cartId).ToList();
            if (cart.Any(c => c.UserId != userId))
                throw new ArgumentException($"Invalid user");

            return cart;
        }

        /// <see cref="ICartService.AddToCart(CartItemViewModel, int)"/>
        /// <exception cref="ArgumentException"/>
        public string AddToCart(CartItemViewModel viewModel, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");

            var cartId = string.IsNullOrEmpty(user.CartId) ? Guid.NewGuid().ToString() : user.CartId ;
            var cartItem = new CartItem(cartId, viewModel.ProductId, viewModel.ProductAmount, userId);
            _context.CartItems.Add(cartItem);
            
            user.CartId = cartId;
            _context.SaveChanges();

            return cartId;
        }

        /// <see cref="ICartService.UpdateCart(CartItemViewModel, int)"/>
        /// <exception cref="ArgumentException"/>
        public void UpdateCart(CartItemViewModel viewModel, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");

            if (string.IsNullOrEmpty(user.CartId))
                throw new ArgumentException("User has no cart to update. Use AddToCart.");

            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == user.CartId && c.ProductId == viewModel.ProductId);
            if (cartItem == null)
                throw new ArgumentException($"Not found. CartId: \"{user.CartId}\", ProductId: \"{viewModel.ProductId}\"");

            cartItem.IsValid = viewModel.IsValid;
            cartItem.ProductAmount = viewModel.ProductAmount;

            _context.SaveChanges();
        }

        /// <see cref="ICartService.RemoveCart(int)"/>
        /// <exception cref="ArgumentException"/>
        public void RemoveCart(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");
            if (string.IsNullOrEmpty(user.CartId))
                throw new ArgumentException("User has no cart to update. Use AddToCart.");

            var cart = _context.CartItems.Where(c => c.Id == user.CartId);
            if (!cart.Any())
                throw new ArgumentException($"No items found for cart with id: \"{user.CartId}\"");

            _context.CartItems.RemoveRange(cart);
            user.CartId = null;
            _context.SaveChanges();
        }

        /// <see cref="ICartService.RemoveFromCart(string, int)"/>
        /// <exception cref="ArgumentException"/>
        public void RemoveFromCart(string productId, int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");
            if (string.IsNullOrEmpty(user.CartId))
                throw new ArgumentException("User has no cart to update. Use AddToCart.");
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentException($"Invalid productId: \"{productId}\"");

            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == user.CartId && c.ProductId == productId);
            if (cartItem == null)
                throw new ArgumentException($"Not found. CartId: \"{user.CartId}\", ProductId: \"{productId}\"");

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }
    }
}
