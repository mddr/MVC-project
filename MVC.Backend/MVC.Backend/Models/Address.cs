using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Backend.Models
{
    public class Address
    {
        [Key] public int Id { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public Address()
        {
        }

        public Address(string city, string postalCode, string street, string houseNumber)
        {
            City = city;
            PostalCode = postalCode;
            Street = street;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            var list = new List<string> {PostalCode, City, Street, HouseNumber};
            return string.Join(";", list);
        }
    }
}
