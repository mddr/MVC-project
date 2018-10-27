using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class CategoryViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? SuperiorCategoryId { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            SuperiorCategoryId = category.SuperiorCategoryId;
            SubCategories = ToList(category.SubCategories);
        }

        public static List<CategoryViewModel> ToList(ICollection<Category> categories)
        {
            return categories?.Select(category => new CategoryViewModel(category)).ToList();
        }
    }
}
