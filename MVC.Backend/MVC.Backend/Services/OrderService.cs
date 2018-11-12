using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void AddOrder(OrderViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var cart = _context.CartItems.Where(i => i.Id == viewModel.CartId).ToList();
            if (cart.Any(c => c.UserId != viewModel.UserId))
                throw new ArgumentException($"Invalid user");

            var order = new Order(viewModel.UserId, viewModel.CartId, viewModel.TotalPrice, cart);

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            if (id < 0)
                throw new ArgumentException();

            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                throw new ArgumentException();

            _context.Orders.Remove(order);
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

        public List<Order> GetOrders(int userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId);
            return orders.ToList();
        }

        public void UpdateOrder(OrderViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var order = _context.Orders.SingleOrDefault(o => o.Id == viewModel.Id);
            if (order == null)
                throw new ArgumentException();

            var cart = _context.CartItems.Where(i => i.Id == viewModel.CartId).ToList();
            if (cart.Any(c => c.UserId != viewModel.UserId))
                throw new ArgumentException($"Invalid user");

            order.TotalPrice = viewModel.TotalPrice;
            order.CartId = viewModel.CartId;
            order.ShoppingCart = cart;


            _context.SaveChanges();
        }
    }
}
