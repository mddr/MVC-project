using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(int categoryId);
        IEnumerable<Product> GetMostPopular(int amount);
        IEnumerable<Product> GetNewest(int? amount);
        IEnumerable<Product> GetDiscounted(int? amount);
        IEnumerable<Product> GetUserHistory(int userId);
        Product GetProduct(string id);
        void AddProduct(ProductViewModel viewModel);
        void UpdateProduct(ProductViewModel viewModel);
        void DeleteProduct(string id);
    }
}
