using MVC.Backend.Helpers;
using MVC.Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane produktu wymieniane między frontem a backendem
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nazwa
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Czy ukryty przed klientami
        /// </summary>
        public bool IsHidden { get; set; }
        /// <summary>
        /// Email eksperta
        /// </summary>
        public string ExpertEmail { get; set; }
        /// <summary>
        /// Opis
        /// </summary>
		public string Description { get; set; }

        /// <summary>
        /// Cena w PLN
        /// </summary>
		public double PricePln { get; set; }
        /// <summary>
        /// Staka VAT
        /// </summary>
        public int TaxRate { get; set; }
        /// <summary>
        /// Zniżka w %
        /// </summary>
        public int Discount { get; set; }

        /// <summary>
        /// Ilość na stanie
        /// </summary>
        public int AmountAvailable { get; set; }
        /// <summary>
        /// Ile razy kupiono
        /// </summary>
        public int BoughtTimes { get; set; }

        /// <summary>
        /// Id kategorii
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Obrazek w base64
        /// </summary>
        public string ImageBase64 { get; set; }
        /// <summary>
        /// Pliki związane z obrazkiem
        /// </summary>
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