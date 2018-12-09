using MVC.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public AddressViewModel()
        {

        }
        public AddressViewModel(Address address)
        {
            Id = address.Id;
            City = address.City;
            PostalCode = address.PostalCode;
            Street = address.Street;
            HouseNumber = address.HouseNumber;
        }
    }
}
