using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za obsługe kategorii
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Pobiera wszystkie kategorie
        /// </summary>
        /// <returns>Kategorie</returns>
        IEnumerable<Category> GetCategories();
        /// <summary>
        /// Pobiera kategorie widoczne przez klientów
        /// </summary>
        /// <returns>Kategorie</returns>
        IEnumerable<Category> GetVisibleCategories();
        /// <summary>
        /// Pobiera kategorie o danym id
        /// </summary>
        /// <param name="id">Id kategorii</param>
        /// <returns>Dane kategorii</returns>
        Category GetCategory(int id);
        /// <summary>
        /// Dodaje nową kategorie o danych podanych w parametrze
        /// </summary>
        /// <param name="viewModel">Dane nowej kategorii</param>
        void AddCategory(CategoryViewModel viewModel);
        /// <summary>
        /// Aktualizuje kategorie zgodnie z danymi w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane kategoriii</param>
        void UpdateCategory(CategoryViewModel viewModel);
        /// <summary>
        /// Usuwa kategorie o podanym id
        /// </summary>
        /// <param name="id">Id kategorii</param>
        void DeleteCategory(int id);
        /// <summary>
        /// Ustawia widoczność kategorii przez klientów
        /// </summary>
        /// <param name="id">Id kategorii</param>
        /// <param name="isVisible">Widczoność do ustawienia</param>
        void SetCategoryVisibility(int id, bool isVisible);
        /// <summary>
        /// Generuje pdf opisujący daną kategorie
        /// </summary>
        /// <param name="id">Id kategorii</param>
        /// <returns>Bajty składające sie na pdf</returns>
        byte[] GeneratePdfSummary(int id);
    }
}
