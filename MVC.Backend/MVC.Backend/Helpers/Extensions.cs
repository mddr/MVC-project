using MVC.Backend.Models;
using System;
using System.IO;

namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Extensions methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Konwertuje plik do base64
        /// </summary>
        /// <param name="product">Metoda dodana do klasy Product</param>
        /// <returns>Plik w postaci base64 lub pusty string</returns>
        public static string GetBase64(this Product product)
        {
            try
            {
                var bytes = File.ReadAllBytes(product.ThumbnailPath);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
