using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Data;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ProductService(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProducts(int categoryId)
        {
            var products = _context.Products.Where(p => p.CategoryId == categoryId);
            return products.ToList();
        }

        public Product GetProduct(string id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                throw new ArgumentException();
            return product;
        }

        public void AddProduct(ProductViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();
            var product = new Product(viewModel.Name, viewModel.PricePln, viewModel.CategoryId,
                viewModel.AmountAvailable, viewModel.ExpertEmail, viewModel.IsHidden, viewModel.TaxRate,
                viewModel.Discount, viewModel.Discount);
            //var imagePath = _fileService.SaveImage(product.Id, viewModel.ImageBase64);
            //var thumbnailPath = _fileService.GenerateThumbnail(product.Id, viewModel.ImageBase64);
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(ProductViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel?.Id))
                throw new ArgumentException();

            var product = _context.Products.SingleOrDefault(p => p.Id == viewModel.Id);
            if (product == null)
                throw new ArgumentException();

            product.Name = viewModel.Name;
            product.CategoryId = viewModel.CategoryId;
            product.AmountAvailable = viewModel.AmountAvailable;
            product.Discount = viewModel.Discount;
            product.ExpertEmail = viewModel.ExpertEmail;
            product.BoughtTimes = viewModel.BoughtTimes;
            product.IsHidden = viewModel.IsHidden;
            product.PricePln = viewModel.PricePln;
            product.TaxRate = viewModel.TaxRate;

            _context.SaveChanges();
        }

        public void DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException();

            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                throw new ArgumentException();

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
