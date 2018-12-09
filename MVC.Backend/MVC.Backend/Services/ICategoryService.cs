using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetVisibleCategories();
        Category GetCategory(int id);
        void AddCategory(CategoryViewModel viewModel);
        void UpdateCategory(CategoryViewModel viewModel);
        void DeleteCategory(int id);
        void SetCategoryVisibility(int id, bool isVisible);
    }
}
