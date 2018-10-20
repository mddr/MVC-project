using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVC.Backend.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public bool IsHidden { get; set; }
        public string ExpertEmail { get; set; }

        [Required] public double PricePln { get; set; }
        [Required] public int TaxRate { get; set; } //todo: walidacja (0/5/8/23)
        public int Discount { get; set; }

        [Required] public DateTime AddedAt => DateTime.Now;
        [Required] public int AmountAvailable { get; set; }
        [Required] public int BoughtTimes { get; set; }

        [Required] public string FullImagePath { get; set; }
        [Required] public string ThumbnailPath { get; set; }

        [ForeignKey("Categories")] public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<ProductFile> Files { get; set; }
    }
}
