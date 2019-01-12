using MVC.Backend.Models;
using System.Text;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane przedmiotu koszyka wymieniane między frontem a backendem
    /// </summary>
    public class CartItemViewModel
    {
        /// <summary>
        /// Id produktu w koszyku
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Ilość produktu w koszyku
        /// </summary>
        public int ProductAmount { get; set; }
        /// <summary>
        /// Czy przedmiot jest poprawny
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Dane produktu
        /// </summary>
        public ProductViewModel Product { get; set; }

        public CartItemViewModel()
        {
        }

        public CartItemViewModel(CartItem cartItem)
        {
            ProductAmount = cartItem.ProductAmount;
            ProductId = cartItem.ProductId;
            Product = new ProductViewModel(cartItem.Product);
            IsValid = cartItem.IsValid;
        }

        public CartItemViewModel(CartItem cartItem, Product product) : this(cartItem)
        {
            Product = new ProductViewModel(product);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Product);
            sb.Append($"Ilość: {ProductAmount}<br>");
            sb.Append("<hr>");

            return sb.ToString();
        }
    }
}
