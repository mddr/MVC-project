using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za obsługę adresów
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// Pobiera wszystkie adresy
        /// </summary>
        /// <returns>Adresy</returns>
        List<Address> GetAddresses();
        /// <summary>
        /// Pobiera adres o podanym id
        /// </summary>
        /// <param name="id">Id adresu</param>
        /// <returns>Adres</returns>
        Address GetAddress(int id);
        /// <summary>
        /// Pobiera adres użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Adres</returns>
        Address GetUserAddress(int userId);
        /// <summary>
        /// Dodaje i zastępuje (jeżeli jest) istniejące adres dla użytkownika o podanym id
        /// </summary>
        /// <param name="viewModel">Dane adresu</param>
        /// <param name="userId">Id użytkownika</param>
        void AddAddress(AddressViewModel viewModel, int userId);
        /// <summary>
        /// Aktualizuje adres użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <param name="viewModel">Nowe dany adresowe</param>
        /// <returns>Task który wywołująca klasa może oczekiwać</returns>
        Task UpdateAddress(int userId, AddressViewModel viewModel);
        /// <summary>
        /// Usuwa adres o podanym id
        /// </summary>
        /// <param name="id">Id adresu</param>
        void DeleteAddress(int id);
    }
}
