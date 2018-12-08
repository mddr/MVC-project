using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC.Backend.Data;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Order AddOrder(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");

            var cart = _context.CartItems.Where(i => i.Id == user.CartId).ToList();
            if (cart == null)
                throw new ArgumentException($"User cart not found. Id: {userId}");

            if (cart.Any(c => c.UserId != userId))
                throw new ArgumentException($"Invalid user");

            double totalPrice = 0;
            foreach(var item in cart)
            {
                var product = _context.Products.FirstOrDefault(u => u.Id == item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product not found. Id: {item.ProductId}");
                totalPrice += product.PricePln * (100 - product.Discount) / 100;
                product.BoughtTimes += item.ProductAmount;
            }

            var order = new Order(userId, user.AddressId, user.CartId, totalPrice, cart);

            user.CartId = null;
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public void DeleteOrder(int id)
        {
            if (id < 0)
                throw new ArgumentException();

            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                throw new ArgumentException();
            var cart = order.ShoppingCart;
            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null)
                    throw new ArgumentException($"Product not found. Id: {item.ProductId}");
                product.BoughtTimes -= item.ProductAmount;
            }

            _context.Orders.Remove(order);
            _context.CartItems.RemoveRange(cart);
            _context.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            var order = _context.Orders.SingleOrDefault(x => x.Id == id);
            if (order == null)
                throw new ArgumentException();
            return order;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public List<OrderViewModel> OrderHistory(int userId)
        {
            var results = new List<OrderViewModel>();
            var orders = _context.Orders.Where(o => o.UserId == userId);
            foreach(Order o in orders)
            {
                var cart = _context.CartItems
                    .Include("Product")
                    .Where(i => i.OrderId == o.Id).ToList();

                results.Add(new OrderViewModel(o, cart));
            }
            return results;
        }

        public List<Order> GetOrders(int userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId);
            return orders.ToList();
        }

        public void UpdateOrder(OrderViewModel viewModel, int userId)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var order = _context.Orders.SingleOrDefault(o => o.Id == viewModel.Id);
            if (order == null)
                throw new ArgumentException();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");


            var cart = _context.CartItems.Where(i => i.Id == user.CartId).ToList();
            if (cart.Any(c => c.UserId != viewModel.UserId))
                throw new ArgumentException($"Invalid user");

            order.TotalPrice = viewModel.TotalPrice;
            order.CartId = user.CartId;
            order.ShoppingCart = cart;


            _context.SaveChanges();
        }
    }
}
