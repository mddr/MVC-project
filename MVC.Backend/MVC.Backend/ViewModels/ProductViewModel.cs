using System;
using System.Collections.Generic;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public string ExpertEmail { get; set; }

        public double PricePln { get; set; }
        public int TaxRate { get; set; }
        public int Discount { get; set; }

        public int AmountAvailable { get; set; }
        public int BoughtTimes { get; set; }

        public string ImageBase64 { get; set; }

        public int CategoryId { get; set; }

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
            PricePln = product.PricePln;
            TaxRate = product.TaxRate;
            Discount = product.Discount;
            AmountAvailable = product.AmountAvailable;
            BoughtTimes = product.BoughtTimes;
        }

        public static List<ProductViewModel> ToList(List<Product> products)
        {
            var viewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                viewModels.Add(new ProductViewModel(product));
            }

            return viewModels;
        }
    }
}