using MVC.Backend.Models;
using System.Collections.Generic;

namespace MVC.Backend.Services
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string CartId { get; set; }

        public double TotalPrice { get; set; }

        public OrderViewModel()
        {
        }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            UserId = order.UserId;
            CartId = order.CartId;
            TotalPrice = order.TotalPrice;
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