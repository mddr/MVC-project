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
        List<Product> GetProducts();
        List<Product> GetProducts(int categoryId);
        Product GetProduct(string id);
        void AddProduct(ProductViewModel viewModel);
        void UpdateProduct(ProductViewModel viewModel);
        void DeleteProduct(string id);
    }
}
