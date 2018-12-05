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
        Task<ObjectResult> Login(LoginViewModel viewModel);
        Task ConfirmEmail(string token);
        UserViewModel GetUserData(int userId);
        User GetUser(int userId);
        List<User> GetUsers();
        void UpdateUser(UserViewModel viewModel);
        void DeleteUser(int userId);
    }
}
