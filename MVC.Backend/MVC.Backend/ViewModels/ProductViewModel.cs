using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVC.Backend.Helpers;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public string ExpertEmail { get; set; }
		public string Description { get; set; }

		public double PricePln { get; set; }
        public int TaxRate { get; set; }
        public int Discount { get; set; }

        public int AmountAvailable { get; set; }
        public int BoughtTimes { get; set; }

        public int CategoryId { get; set; }

        public string ImageBase64 { get; set; }
        public List<FileResultViewModel> Files { get; set; }

        public ProductViewModel()
        {
        }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            CategoryId = product.CategoryId;
            IsHidden = product.IsHidden;
            ExpertEmail = product.ExpertEmail;
			Description = product.Description;
            PricePln = product.PricePln;
            TaxRate = product.TaxRate;
            Discount = product.Discount;
            AmountAvailable = product.AmountAvailable;
            BoughtTimes = product.BoughtTimes;
            ImageBase64 = product.GetBase64();
            if (product.Files != null)
            {
                Files = product.Files.Select(f => new FileResultViewModel(f)).ToList();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Nazwa produktu: {Name}<br>");
            sb.Append($"Email eksperta: {ExpertEmail}<br>");
            var priceAfterDiscount = (1 + (Discount / 100)) * PricePln;
            sb.Append($"Cena: {priceAfterDiscount} zł<br>");
            return sb.ToString();
        }
    }
}