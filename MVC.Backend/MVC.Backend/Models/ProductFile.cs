using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Dane pliku związanego z produktem
    /// </summary>
    public class ProductFile
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public int Id { get; set; }
        /// <summary>
        /// Nazwa pliku
        /// </summary>
        [Required] public string FileName { get; set; }
        /// <summary>
        /// Ścieżka do pliku na serwerze
        /// </summary>
        [Required] public string FilePath { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Required] public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Opis pliku
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Id produktu
        /// </summary>
        [ForeignKey("Product")] public string ProductId { get; set; }
        /// <summary>
        /// Produkt z którym plik jest związany
        /// </summary>
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