using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVC.Backend.Models
{
    public class ProductFile
    {
        [Key] public int Id { get; set; }
        [Required] public string FileName { get; set; }
        [Required] public string FilePath { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        public string Description { get; set; }

        [ForeignKey("Product")] public string ProductId { get; set; }
        public Product Product { get; set; }

        public ProductFile()
        {
            CreatedAt = DateTime.Now;
        }

        public ProductFile(Product product, string fileName, string filePath, string description = null) : this()
        {
            Product = product;
            FileName = fileName;
            FilePath = filePath;
            Description = description;
        }
    }
}