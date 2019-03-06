using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Dane kategorii produktów
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public int Id { get; set; }
        /// <summary>
        /// Nazwa kategorii
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Czy ukryta przed klientami
        /// </summary>
        [Required] public bool IsHidden { get; set; }

        /// <summary>
        /// Id kategorii nadrzędnej
        /// </summary>
        public int? SuperiorCategoryId { get; set; }
        /// <summary>
        /// Kategoria nadrzędna
        /// </summary>
        public Category SuperiorCategory { get; set; }

        /// <summary>
        /// Zbiór podkategorii
        /// </summary>
        public ICollection<Category> SubCategories { get; set; }


        public Category()
        {
            
        }

        public Category(string name, int? superiorCategoryId, bool isHidden = false)
        {
            Name = name;
            SuperiorCategoryId = superiorCategoryId;
            IsHidden = isHidden;
        }
    }
}