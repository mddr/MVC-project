using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Backend.Data;
using MVC.Backend.Helpers;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MVC.Backend.Helpers.Enums;

namespace MVC.Backend.Services
{
    /// <summary>
    /// Implementacja IUserService
    /// </summary>
    /// <see cref="IUserService"/>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IAddressService _addressService;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        /// <param name="tokenService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        /// <param name="addressService">Instancja klasy, tworzona przez DI, implementująca interfejs</param>
        public UserService(ApplicationDbContext context, ITokenService tokenService, IAddressService addressService)
        {
            _context = context;
            _tokenService = tokenService;
			_addressService = addressService;
		}

        /// <see cref="IUserService.AddUser(SignupViewModel, Roles)"/>
        /// <exception cref="ArgumentException"/>
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

        /// <see cref="IUserService.AddUser(SignupViewModel, Roles)"/>
        public IEnumerable<User> GetUsersForNewsletter()
        {
            var users = _context.Users.Where(u => u.AcceptsNewsletters);
            return users;
        }

        /// <see cref="IUserService.GetCurrentUserId(HttpContext)"/>
        public int GetCurrentUserId(HttpContext context)
        {
            return int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        /// <see cref="IUserService.Login(LoginViewModel)"/>
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

        /// <see cref="IUserService.ConfirmEmail(string)"/>
        /// <exception cref="ArgumentException"/>
        public async Task ConfirmEmail(string token)
        {
            var claims = _tokenService.GetPrincipalFromToken(token, false);
            var email = claims.Claims.Where(c => c.Type == ClaimTypes.Email)
                .Select(c => c.Value).SingleOrDefault();

            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
                throw new ArgumentException();

            user.EmailConfirmed = true;
            await _context.SaveChangesAsync();
        }

        /// <see cref="IUserService.GetUserData(int)"/>
        /// <exception cref="ArgumentException"/>
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

        /// <see cref="IUserService.GetUser(int)"/>
        /// <exception cref="ArgumentException"/>
		public User GetUser(int userId)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.Id == userId);
			if (user == null)
				throw new ArgumentException($"User not found. Id: {userId}");
			return user;
		}

        /// <see cref="IUserService.GetUser(string)"/>
        /// <exception cref="ArgumentException"/>
		public User GetUser(string email)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.Email == email);
			if (user == null)
				throw new ArgumentException($"User not found. Email: {email}");
			return user;
		}

        /// <see cref="IUserService.GetUsers"/>
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

        /// <see cref="IUserService.UpdateUser(UserViewModel)"/>
        /// <exception cref="ArgumentException"/>
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

        /// <see cref="IUserService.DeleteUser(int)"/>
        /// <exception cref="ArgumentException"/>
		public void DeleteUser(int userId)
		{
			var user = _context.Users.SingleOrDefault(c => c.Id == userId);
			if (user == null)
				throw new ArgumentException("Invalid id");
			_context.Users.Remove(user);
			_context.SaveChanges();
		}

        /// <see cref="IUserService.ChangePassword(int, string, string)"/>
        /// <exception cref="ArgumentException"/>
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

        /// <see cref="IUserService.SetPassword(int, string, string)"/>
        /// <exception cref="ArgumentException"/>
        public async Task SetPassword(int userId, string newPassword, string token)
        {
            var user = _context.Users.Single(u => u.Id == userId);

            var claims = _tokenService.GetPrincipalFromToken(token, true);
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
