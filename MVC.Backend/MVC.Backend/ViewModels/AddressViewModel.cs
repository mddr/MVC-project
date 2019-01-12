using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane adresu wymieniane między frontem a backendem
    /// </summary>
    public class AddressViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Miasto
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
