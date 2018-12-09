using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Backend.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public bool IsHidden { get; set; }

        public int? SuperiorCategoryId { get; set; }
        public Category SuperiorCategory { get; set; }

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