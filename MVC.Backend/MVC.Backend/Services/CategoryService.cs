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
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories
                .Include("SubCategories")
                .ToList();
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories
                .Include("SubCategories")
                .SingleOrDefault(c => c.Id == id);
            if (category == null)
                throw new ArgumentException("Invalid id");
            return category;
        }

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

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
                throw new ArgumentException("Invalid id");
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public bool Exists(int? id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
}
