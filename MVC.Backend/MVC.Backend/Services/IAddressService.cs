using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;

namespace MVC.Backend.Services
{
    public interface IAddressService
    {
        List<Address> GetAddresses();
        Address GetAddress(int id);
        Address GetUserAddress(int userId);
        void AddAddress(AddressViewModel viewModel, int userId);
        void UpdateAddress(AddressViewModel viewModel);
        void DeleteAddress(int id);
    }
}
