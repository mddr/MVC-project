﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Backend.Data;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using static MVC.Backend.Helpers.Enums;

namespace MVC.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IAddressService _addressService;

        public UserService(ApplicationDbContext context, ITokenService tokenService, IAddressService addressService)
        {
            _context = context;
            _tokenService = tokenService;
			_addressService = addressService;
		}

        public async Task AddUser(SignupViewModel viewModel, Roles role = Roles.User)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == viewModel.Email);
            if (user != null)
                throw new ArgumentException();

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(viewModel.Password, salt);
            var newUser = new User(viewModel.Email, passwordHash, salt, viewModel.FirstName, viewModel.LastName, viewModel.AddressId, role: role);
            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> GetUsersForNewsletter()
        {
            var users = _context.Users.Where(u => u.AcceptsNewsletters);
            return users;
        }

        public async Task<ObjectResult> Login(LoginViewModel viewModel)
        {
            var user = AuthHelper.Authenticate(viewModel.Email, viewModel.Password, _context);
            if (user == null)
                throw new ArgumentException();

            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Confirmed", user.EmailConfirmed.ToString()),
            };

            var jwtToken = _tokenService.GenerateAccessToken(userClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            await _context.SaveChangesAsync();

            return new ObjectResult(new
            {
                token = jwtToken,
                refreshToken
            });
        }

        public async Task ConfirmEmail(string token)
        {
            var claims = _tokenService.GetPrincipalFromExpiredToken(token);
            var email = claims.Claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();

            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                throw new ArgumentException();

            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();
        }

        public UserViewModel GetUserData(int userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User with id {userId} not found");
            if (user.AddressId.HasValue)
                user.Address = _context.Addresses.First(a => a.Id == user.AddressId);
            var result = new UserViewModel(user);
            return result;
        }

		public User GetUser(int userId)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.Id == userId);
			if (user == null)
				throw new ArgumentException($"User not found. Id: {userId}");
			return user;
		}

		public User GetUser(string email)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.Email == email);
			if (user == null)
				throw new ArgumentException($"User not found. Email: {email}");
			return user;
		}

		public IEnumerable<User> GetUsers()
		{
			var users = _context.Users;
			var addresses = _addressService.GetAddresses();
			foreach(var a in addresses)
			{
				var user = users.SingleOrDefault(u => u.AddressId == a.Id);
				if (user != null){
					user.Address = a;
				}
			}
			return users.ToList();
		}

		public void UpdateUser(UserViewModel viewModel)
		{
			if (viewModel == null)
				throw new ArgumentException();

			var user = _context.Users.SingleOrDefault(c => c.Id == viewModel.Id);
			if (user == null)
				throw new ArgumentException("Invalid id");

			if (!Enum.TryParse(viewModel.Currency, out Currency currency))
				throw new ArgumentException("Faild to parse currency");

			user.Currency = currency;
			user.EmailConfirmed = viewModel.EmailConfirmed;
			user.FirstName = viewModel.FirstName;
			user.LastName = viewModel.LastName;
			user.Email = viewModel.Email;
			user.PrefersNetPrice = viewModel.PrefersNetPrice;
			user.AcceptsNewsletters = viewModel.AcceptsNewsletters;
			user.ProductsPerPage = viewModel.ProductsPerPage;
			user.AcceptsNewsletters = viewModel.AcceptsNewsletters;

			_context.SaveChanges();
		}

		public void DeleteUser(int userId)
		{
			var user = _context.Users.SingleOrDefault(c => c.Id == userId);
			if (user == null)
				throw new ArgumentException("Invalid id");
			_context.Users.Remove(user);
			_context.SaveChanges();
		}

        public async Task ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = _context.Users.Single(u => u.Id == userId);
            var validatedUser = AuthHelper.Authenticate(user.Email, oldPassword, _context);
            if (validatedUser == null)
                throw new ArgumentException($"Invalid old password provided");

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(newPassword, salt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync();
        }

        public async Task SetPassword(int userId, string newPassword, string token)
        {
            var user = _context.Users.Single(u => u.Id == userId);

            var claims = _tokenService.GetPrincipalFromExpiredToken(token);
            var email = claims.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            var type = claims.Claims.First(c => c.Type == "type").Value;
            if (type != "passwordReset" || email != user.Email)
                throw new ArgumentException("Invalid token");

            var salt = AuthHelper.CreateSalt(128);
            var passwordHash = AuthHelper.CreateHash(newPassword, salt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync();
        }
    }
}
