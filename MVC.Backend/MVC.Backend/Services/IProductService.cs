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
        IEnumerable<Product> GetProducts(bool? isHidden);
        IEnumerable<Product> GetProducts(int categoryId, bool? isHidden);
        IEnumerable<Product> GetMostPopular(int amount, bool isHidden);
        IEnumerable<Product> GetNewest(int? amount, bool isHidden);
        IEnumerable<Product> GetDiscounted(int? amount, bool isHidden);
        IEnumerable<Product> GetUserHistory(int userId);
        Product GetProduct(string id);
        void AddProduct(ProductViewModel viewModel);
        void UpdateProduct(ProductViewModel viewModel);
        void DeleteProduct(string id);
        void SetProductVisibility(string id, bool isVisible);
    }
}
