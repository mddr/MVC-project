using MVC.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders();
        List<Order> GetOrders(int userId);
        Order GetOrder(int id);
        void AddOrder(OrderViewModel viewModel);
        void UpdateOrder(OrderViewModel viewModel);
        void DeleteOrder(int id);
    }
}
