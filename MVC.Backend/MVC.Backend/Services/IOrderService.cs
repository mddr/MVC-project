using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowada za operacje na zamówieniach
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Pobiera wszystkie zamówienia 
        /// </summary>
        /// <returns>Zamówienia</returns>
        List<Order> GetOrders();
        /// <summary>
        /// Pobiera zamówienia użytkownika o podanym id
        /// </summary>  
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Zamówienia</returns>
        List<Order> GetOrders(int userId);
        /// <summary>
        /// Pobiera zamówienie o podanym id
        /// </summary>
        /// <param name="id">Id zamówienia</param>
        /// <returns></returns>
        Order GetOrder(int id);
        /// <summary>
        /// Pobiera historie zamówień użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Historia zamówień w postaci listy kupionych koszyków</returns>
        List<OrderViewModel> OrderHistory(int userId);
        /// <summary>
        /// Zamawia zawartość koszyk użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <returns>Złożone zamówienie</returns>
        Order AddOrder(int userId);
        /// <summary>
        /// Aktualizuje zamówienie użytkownika o podanym id zgodnie z danym przekazanymi w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane zamówienia</param>
        /// <param name="userId">Id użytkownika</param>
        void UpdateOrder(OrderViewModel viewModel, int userId);
        /// <summary>
        /// Usuwa zamówienie o podanym id
        /// </summary>
        /// <param name="id">Id zamówienia</param>
        void DeleteOrder(int id);
    }
}
