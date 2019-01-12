using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Backend.Models
{
    /// <summary>
    /// Adres dostawy
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key] public int Id { get; set; }
        /// <summary>
        /// Nazwa miasta
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Kod pocztowy
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Nazwa ulicy
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Nr domu
        /// </summary>
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
