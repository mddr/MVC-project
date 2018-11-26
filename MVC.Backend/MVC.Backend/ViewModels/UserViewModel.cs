using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressViewModel Address { get; set; }

        public string Currency { get; set; }
        public bool PrefersNetPrice { get; set; }
        public bool AcceptsNewsletters { get; set; }
        public int ProductsPerPage { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(User user)
        {
            Id = user.Id;
            EmailConfirmed = user.EmailConfirmed;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Currency = user.Currency.ToString();
            PrefersNetPrice = user.PrefersNetPrice;
            AcceptsNewsletters = user.AcceptsNewsletters;
            ProductsPerPage = user.ProductsPerPage;
            if (user.Address != null) Address = new AddressViewModel(user.Address);
        }
    }
}
