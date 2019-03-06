using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Odpowiada za operacje na klasie {User}
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronicznie dodaje nowego użytkownika
        /// </summary>
        /// <param name="viewModel">Dane użytkownika</param>
        /// <param name="role">Rola użytkownika</param>
        /// <returns>Task na który klasa wywołująca może czekać</returns>
        Task AddUser(SignupViewModel viewModel, Enums.Roles role = Enums.Roles.User);
        /// <summary>
        /// Pobiera wybrane dane użytkownika o id podanym w parametrze
        /// </summary>
        /// <param name="userId">Id uzytkownika</param>
        /// <returns>Dane użytkownika</returns>
        UserViewModel GetUserData(int userId);
        /// <summary>
        /// Pobiera wszystkie dane użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id uzytkownika</param>
        /// <returns>Dane użytkownika</returns>
        User GetUser(int userId);
        /// <summary>
        /// Pobiera wszystkie dane użytkownika o podanym emailu
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <returns>Dane użytkownika</returns>
        User GetUser(string email);
        /// <summary>
        /// Pobiera wszystkich użytkowników
        /// </summary>
        /// <returns>Wszyscy użytkownicy</returns>
        IEnumerable<User> GetUsers();
        /// <summary>
        /// Pobiera użytkowników akceptujących wysyłanie newslettara
        /// </summary>
        /// <returns>Użytkownicy akceptjący newsletter</returns>
        IEnumerable<User> GetUsersForNewsletter();
        /// <summary>
        /// Pobiera id zalogowanego użytkownika
        /// </summary>
        /// <param name="context">Kontekst</param>
        /// <returns>Id zalogowanego użytkownika</returns>
        int GetCurrentUserId(HttpContext context);

        /// <summary>
        /// Zalogowuje użytkownika jeżeli przekazane dane są poprawne
        /// </summary>
        /// <param name="viewModel">Dane logowania</param>
        /// <returns>JTW refersh token</returns>
        Task<ObjectResult> Login(LoginViewModel viewModel);
        /// <summary>
        /// Potwierdza mail zalogowanego użytkownika jeżeli podany token jest poprawny
        /// </summary>
        /// <param name="token">Token potwierdzający</param>
        /// <returns>Task który klasa wywołująca może oczekiwać</returns>
        Task ConfirmEmail(string token);
        /// <summary>
        /// Aktualizuje dane użytkownika zgodnie z danymi w parametrze
        /// </summary>
        /// <param name="viewModel">Nowe dane użytkownika</param>
        void UpdateUser(UserViewModel viewModel);
        /// <summary>
        /// Usuwa użytkownika o podanym id
        /// </summary>
        /// <param name="userId">Id użytkownika do usunięcia</param>
        void DeleteUser(int userId);
        /// <summary>
        /// Zmienia hasło użytkownika o podanym id jeżeli stare hasło jest poprawne
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <param name="oldPassword">Stare hasło</param>
        /// <param name="newPassword">Nowe hasło</param>
        /// <returns></returns>
        Task ChangePassword(int userId, string oldPassword, string newPassword);
        /// <summary>
        /// Ustawia hasło użytkownika o podanym id jeżeli token jest poprawny
        /// </summary>
        /// <param name="userId">Id użytkownika</param>
        /// <param name="newPassword">Nowe hasło</param>
        /// <param name="token">Token potwierdzający</param>
        /// <returns></returns>
        Task SetPassword(int userId, string newPassword, string token);
    }
}
