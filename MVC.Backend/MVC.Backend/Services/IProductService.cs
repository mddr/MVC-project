using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za operacje na produktach
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Pobiera wszystkie produkty jezeli parametr jest null
        /// W przeciwnym wypadku zwraca produkty z widocznością określoną przez parametr
        /// </summary>
        /// <param name="isHidden">Widoczność produktów</param>
        /// <returns>Produkty</returns>
        IEnumerable<Product> GetProducts(bool? isHidden);
        /// <summary>
        /// Pobiera wszystkie produkty z kategorii jezeli parametr jest null
        /// W przeciwnym wypadku zwraca produkty z kategorii z widocznością określoną przez parametr
        /// </summary>
        /// <param name="categoryId">Id kategorii</param>
        /// <param name="isHidden">Widczoność produktów</param>
        /// <returns>Produkty</returns>
        IEnumerable<Product> GetProducts(int categoryId, bool? isHidden);
        /// <summary>
        /// Pobiera {amount} najpopularniejszych produktów o okreslonej widoczności
        /// </summary>
        /// <param name="amount">Ilość do pobrania</param>
        /// <param name="isHidden">Widoczność produktów</param>
        /// <returns>Najpopularnijesze produkty</returns>
        IEnumerable<Product> GetMostPopular(int amount, bool isHidden);
        /// <summary>
        /// Pobiera {amount} najnowszych produktów o okreslonej widoczności
        /// </summary>
        /// <param name="amount">Ilość do pobrania</param>
        /// <param name="isHidden">Widoczność produktów</param>
        /// <returns>Najnowsze produkty</returns>
        IEnumerable<Product> GetNewest(int? amount, bool isHidden);
        /// <summary>
        /// Pobiera {amount} przecenionych produktów o okreslonej widoczności
        /// </summary>
        /// <param name="amount">Ilość do pobrania</param>
        /// <param name="isHidden">Widoczność produktów</param>
        /// <returns>Najnowsze produkty</returns>
        IEnumerable<Product> GetDiscounted(int? amount, bool isHidden);
        /// <summary>
        /// Pobiera historie zamówień użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Historia zakupów</returns>
        IEnumerable<Product> GetUserHistory(int userId);
        /// <summary>
        /// Pobiera produkt o podanym id
        /// </summary>
        /// <param name="id">Id produktu</param>
        /// <returns>Produkt</returns>
        Product GetProduct(string id);
        /// <summary>
        /// Dodaje produkt o danych podanych w parametrze
        /// </summary>
        /// <param name="viewModel">Dane nowego produktu</param>
        void AddProduct(ProductViewModel viewModel);
        /// <summary>
        /// Aktualizuje produkt o danych podanych w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane produktu</param>
        void UpdateProduct(ProductViewModel viewModel);
        /// <summary>
        /// Usuwa produkt o podanym id
        /// </summary>
        /// <param name="id">Id produktu</param>
        void DeleteProduct(string id);
        /// <summary>
        /// Zmienia widoczność produkt o podanym id na podaną w parametrze
        /// </summary>
        /// <param name="id">Id produktu</param>
        /// <param name="isVisible">Nowa widoczność produktu</param>
        void SetProductVisibility(string id, bool isVisible);
        /// <summary>
        /// Dodaje plik do produktu o id określonym w parametrze
        /// </summary>
        /// <param name="viewModel">Dane pliku oraz id produktu do którego należy plik</param>
        /// <returns>Task który wywołujący może oczekiwać</returns>
        Task AddFile(FileRequestViewModel viewModel);
        /// <summary>
        /// Usuwa plik o podanym id z produktu o podanym id
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="fileId">Id pliku</param>
        /// <returns>Task który wywołujący może oczekiwać</returns>
        Task DeleteFile(string productId, int fileId);
        /// <summary>
        /// Pobiera dane pliku związanego z produktem o podanym id
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="fileId">Id pliku</param>
        /// <returns>Dane pliku</returns>
        ProductFile GetFile(string productId, int fileId);
    }
}
