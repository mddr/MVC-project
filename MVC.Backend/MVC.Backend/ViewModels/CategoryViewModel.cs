using MVC.Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane kategorii wymieniane między frontem a backendem
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Nazwa
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Czy jest ukryta przed klientami
        /// </summary>
        public bool IsHidden { get; set; }
        /// <summary>
        /// Id kategorii nadrzędnej
        /// </summary>
        public int? SuperiorCategoryId { get; set; }
        /// <summary>
        /// Lista podkategorii
        /// </summary>
        public List<CategoryViewModel> SubCategories { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
			IsHidden = category.IsHidden;
            SuperiorCategoryId = category.SuperiorCategoryId;
            if (category.SubCategories != null)
                SubCategories = category.SubCategories.Select(c => new CategoryViewModel(c)).ToList();
        }
    }
}
