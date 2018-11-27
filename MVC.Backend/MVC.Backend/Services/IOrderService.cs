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
        List<Order> OrderHistory(int userId);
        void AddOrder(int userId);
        void UpdateOrder(OrderViewModel viewModel, int userId);
        void DeleteOrder(int id);
    }
}
