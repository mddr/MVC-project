using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using MVC.Backend.Data;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MVC.Backend.Services
{
    /// <see cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        /// <param name="productService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public CategoryService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        /// <see cref="ICategoryService.GetCategories"/>
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories
                .Include("SubCategories")
                .ToList();
        }

        /// <see cref="ICategoryService.GetVisibleCategories"/>
        public IEnumerable<Category> GetVisibleCategories()
        {
            return _context.Categories
                .Include("SubCategories")
                .Where(c => !c.IsHidden)
                .ToList();
        }

        /// <see cref="ICategoryService.GetCategory(int)"/>
        /// <exception cref="ArgumentException"/>
        public Category GetCategory(int id)
        {
            var category = _context.Categories
                .Include("SubCategories")
                .SingleOrDefault(c => c.Id == id);
            if (category == null)
                throw new ArgumentException("Invalid id");
            return category;
        }

        /// <see cref="ICategoryService.AddCategory(CategoryViewModel)"/>
        /// <exception cref="ArgumentException"/>
        public void AddCategory(CategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();
            if (viewModel.SuperiorCategoryId.HasValue && !Exists(viewModel.SuperiorCategoryId))
                throw new ArgumentException();

            var category = new Category(viewModel.Name, viewModel.SuperiorCategoryId);
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        /// <see cref="ICategoryService.UpdateCategory(CategoryViewModel)"/>
        /// <exception cref="ArgumentException"/>
        public void UpdateCategory(CategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var category = _context.Categories.SingleOrDefault(c => c.Id == viewModel.Id);
            if (category == null)
                throw new ArgumentException("Invalid id");

            category.Name = viewModel.Name;
            if (viewModel.SuperiorCategoryId != null) category.SuperiorCategoryId = viewModel.SuperiorCategoryId.Value;
            _context.SaveChanges();
        }

        /// <see cref="ICategoryService.DeleteCategory(int)"/>
        /// <exception cref="ArgumentException"/>
        public void DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                throw new ArgumentException("Invalid id");
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        /// <see cref="ICategoryService.SetCategoryVisibility(int, bool)"/>
        public void SetCategoryVisibility(int id, bool isVisible)
        {
            var category = GetCategory(id);
            category.IsHidden = !isVisible;
            var products = _productService.GetProducts(id, null);
            foreach (var product in products)
            {
                product.IsHidden = !isVisible;
            }

            _context.SaveChanges();
            foreach (var subCategory in category.SubCategories)
            {
                SetCategoryVisibility(subCategory.Id, isVisible);
            }
        }

        /// <see cref="ICategoryService.GeneratePdfSummary(int)"/>
        public byte[] GeneratePdfSummary(int id)
        {
            var category = GetCategory(id);
            var products = _productService.GetProducts(id, null);

            var document = new Document(PageSize.A4, 50, 50, 50, 50);

            using (var output = new MemoryStream())
            {
                PdfWriter.GetInstance(document, output);
                document.Open();

                var header = new Paragraph($"{category.Name} - cennik") { Alignment = Element.ALIGN_CENTER };
                document.Add(header);

                foreach (var product in products)
                {
                    var viewModel = new ProductViewModel(product);
                    var viewModelString = viewModel.ToString()
                        .Replace("<br>", "; ")
                        .Replace("<hr>", string.Empty)
                        .Replace("zł", "zl");
                    var paragraph = new Paragraph(viewModelString + "\n");
                    document.Add(paragraph);
                }

                document.Close();
                var bytes = output.ToArray();
                return bytes;
            }
        }

        /// <summary>
        /// Sprawdza czy kategoria o podanym id istenije
        /// </summary>
        /// <param name="id">Id kategorii</param>
        /// <returns>Czy kategoria istnieje</returns>
        public bool Exists(int? id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
}
