using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface IUserService
    {
        Task AddUser(SignupViewModel viewModel, Enums.Roles role = Enums.Roles.User);
        UserViewModel GetUserData(int userId);
        User GetUser(int userId);
        User GetUser(string email);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersForNewsletter();

        Task<ObjectResult> Login(LoginViewModel viewModel);
        Task ConfirmEmail(string token);
        void UpdateUser(UserViewModel viewModel);
        void DeleteUser(int userId);
        Task ChangePassword(int userId, string oldPassword, string newPassword);
        Task SetPassword(int userId, string newPassword, string token);
    }
}
