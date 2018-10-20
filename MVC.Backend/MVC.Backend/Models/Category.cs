using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Backend.Models
{
    public class Category
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public int SuperiorCategoryId { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}