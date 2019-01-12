using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Dane produktu
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public string Id { get; set; }

        /// <summary>
        /// Nazwa produktu
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Czy jest ukryty przed klientami
        /// </summary>
        [Required] public bool IsHidden { get; set; }
        /// <summary>
        /// Mail eksperta
        /// </summary>
        public string ExpertEmail { get; set; }
        /// <summary>
        /// Opis produktu
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Cena w PLN
        /// </summary>
        [Required] public double PricePln { get; set; }
        /// <summary>
        /// Stawka VAT
        /// </summary>
        [Required] public int TaxRate { get; set; } //todo: walidacja (0/5/8/23)
        /// <summary>
        /// Zniżka w %
        /// </summary>
        public int Discount { get; set; }

        /// <summary>
        /// Data utworzenia
        /// </summary>
        [Required] public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ilość na stanie
        /// </summary>
        [Required] public int AmountAvailable { get; set; }
        /// <summary>
        /// Ile razy kupiono
        /// </summary>
        [Required] public int BoughtTimes { get; set; }

        /// <summary>
        /// Ścieżka do obrazka
        /// </summary>
        public string FullImagePath { get; set; }
        /// <summary>
        /// Ścieżka do miniaturki
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Id kategorii
        /// </summary>
        [ForeignKey("Categories")] public int CategoryId { get; set; }
        /// <summary>
        /// Kategoria w której produkt sie znajduje
        /// </summary>
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
