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
        [Key] public string Id { get; set; }

        [Required] public string Name { get; set; }
        [Required] public bool IsHidden { get; set; }
        public string ExpertEmail { get; set; }
        public string Description { get; set; }

        [Required] public double PricePln { get; set; }
        [Required] public int TaxRate { get; set; } //todo: walidacja (0/5/8/23)
        public int Discount { get; set; }

        [Required] public DateTime CreatedAt { get; set; }
        [Required] public int AmountAvailable { get; set; }
        [Required] public int BoughtTimes { get; set; }

        public string FullImagePath { get; set; }
        public string ThumbnailPath { get; set; }

        [ForeignKey("Categories")] public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<ProductFile> Files { get; set; }

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Product(string name, double pricePln, int categoryId, int amountAvailable, string expertEmail, string description, bool isHidden = false, int taxRate = 23, int discount = 0,
            int boughtTimes = 0, string fullImagePath = null, string thumbnailPath = null) : this()
        {
            CreatedAt = DateTime.Now;
            Name = name;
            IsHidden = isHidden;
            ExpertEmail = expertEmail;
			Description = description;
            PricePln = pricePln;
            TaxRate = taxRate;
            Discount = discount;
            AmountAvailable = amountAvailable;
            BoughtTimes = boughtTimes;
            FullImagePath = fullImagePath;
            ThumbnailPath = thumbnailPath;
            CategoryId = categoryId;
        }
    }
}
