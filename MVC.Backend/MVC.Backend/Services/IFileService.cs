using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Obsługuje pliki
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Zwraca zawartość pliku o podanej ścieżce
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku</param>
        /// <returns>Zawartość pliku w bajtach oraz typ pliku</returns>
        FileContent GetFileContent(string filePath);
        /// <summary>
        /// Zaspisuje plik związany z produktem 
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="base64">Zawartość pliku</param>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Ścieżka do pliku</returns>
        string SaveFile(string productId, string base64, string fileName);
        /// <summary>
        /// Zapisuje obrazek związany z produktem
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="base64">Obrazek w postaci base64</param>
        /// <returns>Ścieżka do obrazka</returns>
        string SaveImage(string productId, string base64);
        /// <summary>
        /// Zapisuje minaturke związaną z produktem
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="base64">Miniaturka w postaci base64</param>
        /// <returns>Ścieżka do miniaturki</returns>
        string SaveThumbnail(string productId, string base64);
        /// <summary>
        /// Usuwa plik o podanej ścieżce
        /// </summary>
        /// <param name="filePath">Scieżka do pliku</param>
        void DeleteFile(string filePath);
    }
}
