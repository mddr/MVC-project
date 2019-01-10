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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ProductService(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public IEnumerable<Product> GetProducts(bool? isHidden)
        {
            var products = _context.Products.Include(p => p.Files);
            return isHidden.HasValue
                ? products.Where(p => p.IsHidden == isHidden.Value)
                : products;
        }

        public IEnumerable<Product> GetProducts(int categoryId, bool? isHidden)
        {
            var products = _context.Products.Include(p => p.Files).Where(p => p.CategoryId == categoryId);
            return isHidden.HasValue
                ? products.Where(p => p.IsHidden == isHidden.Value)
                : products;
        }

        public IEnumerable<Product> GetMostPopular(int amount, bool isHidden)
        {
            var products = _context.Products.Include(p => p.Files).Where(p => p.IsHidden == isHidden);
            return products.OrderByDescending(p => p.BoughtTimes).Take(amount).ToList();
        }

		public IEnumerable<Product> GetNewest(int? amount, bool isHidden)
		{
			var products = _context.Products.Include(p => p.Files).Where(p => p.IsHidden == isHidden).OrderByDescending(p => p.CreatedAt);
		    return amount.HasValue
		        ? products.Take(amount.Value).ToList()
		        : products.ToList();
        }

        public IEnumerable<Product> GetDiscounted(int? amount, bool isHidden)
        {
            var products = _context.Products
                .Include(p => p.Files)
                .Where(p => p.Discount > 0 && p.IsHidden == isHidden)
                .OrderByDescending(p => p.CreatedAt);

            return amount.HasValue
                ? products.Take(amount.Value).ToList()
                : products.ToList();
        }

        public IEnumerable<Product> GetUserHistory(int userId)
        {
            var orders = _context.Orders.Where(o => o.UserId == userId);
            if (!orders.Any()) return null;

            var products = new List<Product>();
            foreach (var order in orders)
            {
                var cart = _context.CartItems
                    .Include("Product")
                    .Where(c => c.Id == order.CartId);
                foreach (var cartItem in cart)
                {
                    products.Add(cartItem.Product);
                }
            }

            return products;
        }

        public Product GetProduct(string id)
        {
            var product = _context.Products.
				Include(p => p.Files)
				.SingleOrDefault(p => p.Id == id);
            if (product == null)
                throw new ArgumentException();
            return product;
        }

        public void AddProduct(ProductViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();
            var product = new Product(viewModel.Name, viewModel.PricePln, viewModel.CategoryId,
                viewModel.AmountAvailable, viewModel.ExpertEmail, viewModel.Description, viewModel.IsHidden, viewModel.TaxRate,
                viewModel.Discount);

            if (!string.IsNullOrEmpty(viewModel.ImageBase64))
            {
                product.FullImagePath = _fileService.SaveImage(product.Id, viewModel.ImageBase64);
                product.ThumbnailPath = _fileService.SaveThumbnail(product.Id, viewModel.ImageBase64);
            }

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
            product.Description = viewModel.Description;
            product.BoughtTimes = viewModel.BoughtTimes;
            product.IsHidden = viewModel.IsHidden;
            product.PricePln = viewModel.PricePln;
            product.TaxRate = viewModel.TaxRate;

            if (!string.IsNullOrEmpty(viewModel.ImageBase64))
            {
                product.FullImagePath = _fileService.SaveImage(product.Id, viewModel.ImageBase64);
                product.ThumbnailPath = _fileService.SaveThumbnail(product.Id, viewModel.ImageBase64);
            }

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

        public void SetProductVisibility(string id, bool isVisible)
        {
            var product = GetProduct(id);
            product.IsHidden = !isVisible;
            _context.SaveChanges();
        }

        public async Task AddFile(FileRequestViewModel viewModel)
        {
            var product = GetProduct(viewModel.ProductId);
            var filePath = _fileService.SaveFile(product.Id, viewModel.Base64, viewModel.FileName);
            var file = new ProductFile(product, viewModel.FileName, filePath, viewModel.Description);
            _context.ProductFiles.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFile(string productId, int fileId)
        {
            var file = _context.ProductFiles.Single(f => f.ProductId == productId && f.Id == fileId);
            _fileService.DeleteFile(file.FilePath);
            _context.ProductFiles.Remove(file);
            await _context.SaveChangesAsync();
        }

        public ProductFile GetFile(string productId, int fileId)
        {
            var file = _context.ProductFiles.Single(f => f.ProductId == productId && f.Id == fileId);
            return file;
        }
    }
}
