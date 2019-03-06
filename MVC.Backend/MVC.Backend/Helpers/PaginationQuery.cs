using System.Collections.Generic;
using System.Linq;

namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Zapytanie do paginacji stron produktów
    /// </summary>
    /// <typeparam name="T">Jakaś klasa</typeparam>
    public class PaginationQuery<T>
    {
        /// <summary>
        /// Nr strony
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Rozmiar jednej strony
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Pobiera {Size} przedmiotów pomijając przedmioty z poprzednich stron
        /// </summary>
        /// <param name="items">Zbiór przedmiotów</param>
        /// <returns></returns>
        public IEnumerable<T> Paginate(IEnumerable<T> items) => items.Skip(Page * Size).Take(Size);        
    }
}
