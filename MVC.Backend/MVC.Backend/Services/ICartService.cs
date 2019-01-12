using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za operacje na koszyku
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Pobiera zawartość koszyka użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Koszyk</returns>
        List<CartItem> GetCartItems(int userId);
        /// <summary>
        /// Dodaje przedmiot do koszyka użytkownika o podanym id
        /// </summary>
        /// <param name="viewModel">Przedmiot dodawany</param>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Zwraca id koszyka</returns>
        string AddToCart(CartItemViewModel viewModel, int userId); //returns CartId
        /// <summary>
        /// Aktualizuje przedmiot w koszyku użytkownika o podanym id
        /// </summary>
        /// <param name="viewModel">Nowe dane przedmiotu</param>
        /// <param name="userId">Id użytkownika</param>
        void UpdateCart(CartItemViewModel viewModel, int userId);
        /// <summary>
        /// Usuwa aktualny koszyk użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        void RemoveCart(int userId);
        /// <summary>
        /// Usuwa przedmiot z koszyka użytkowniak o podanym id
        /// </summary>
        /// <param name="productId">Id produktu do usunięcia z koszyka</param>
        /// <param name="userId">Id użytkownika</param>
        void RemoveFromCart(string productId, int userId);
    }
}
