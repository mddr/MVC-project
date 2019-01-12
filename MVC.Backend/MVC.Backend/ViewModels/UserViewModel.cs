using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane użytkownika przesyłane na frontend
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Czy potwierdzono email
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Nazwisko
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
		public string Email { get; set; }
        /// <summary>
        /// Dane adresu użytkownika
        /// </summary>
		public AddressViewModel Address { get; set; }

        /// <summary>
        /// Preferowana waluta
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Czy preferuje ceny netto
        /// </summary>
        public bool PrefersNetPrice { get; set; }
        /// <summary>
        /// Czy akceptuje newsletter
        /// </summary>
        public bool AcceptsNewsletters { get; set; }
        /// <summary>
        /// Produkty na strone
        /// </summary>
        public int ProductsPerPage { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(User user)
        {
            Id = user.Id;
            EmailConfirmed = user.EmailConfirmed;
            FirstName = user.FirstName;
            LastName = user.LastName;
			Email = user.Email;
            Currency = user.Currency.ToString();
            PrefersNetPrice = user.PrefersNetPrice;
            AcceptsNewsletters = user.AcceptsNewsletters;
            ProductsPerPage = user.ProductsPerPage;
            if (user.Address != null) Address = new AddressViewModel(user.Address);
        }
	}
}
