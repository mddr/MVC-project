using System.Collections.Generic;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int? AddressId { get; set; }

        public double TotalPrice { get; set; }

        public OrderViewModel()
        {
        }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            UserId = order.UserId;
            AddressId = order.AddressId;
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