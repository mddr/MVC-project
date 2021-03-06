﻿using MVC.Backend.Data;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Services
{
    /// <see cref="IAddressService"/>
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Kontekst bazodanowy</param>
        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <see cref="IAddressService.AddAddress(AddressViewModel, int)"/>
        /// <exception cref="ArgumentException"/>
        public void AddAddress(AddressViewModel viewModel, int userId)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ArgumentException($"User not found. Id: {userId}");

            var address = new Address(viewModel.City, viewModel.PostalCode, viewModel.Street, viewModel.HouseNumber);

            _context.Addresses.Add(address);
            _context.SaveChanges();
            user.AddressId = address.Id;
            _context.SaveChanges();
        }

        /// <see cref="IAddressService.DeleteAddress(int)"/>
        /// <exception cref="ArgumentException"/>
        public void DeleteAddress(int id)
        {
            var address = _context.Addresses.SingleOrDefault(c => c.Id == id);
            if (address == null)
                throw new ArgumentException("Invalid id");
            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }

        /// <see cref="IAddressService.GetAddress(int)"/>
        /// <exception cref="ArgumentException"/>
        public Address GetAddress(int id)
        {
            var address = _context.Addresses.SingleOrDefault(p => p.Id == id);
            if (address == null)
                throw new ArgumentException("Invalid id");
            return address;
        }

        /// <see cref="IAddressService.GetAddresses"/>
        public List<Address> GetAddresses()
        {
            return _context.Addresses.ToList();
        }

        /// <see cref="IAddressService.GetUserAddress(int)"/>
        /// <exception cref="ArgumentException"/>
        public Address GetUserAddress(int userId)
        {
            var user = _context.Users.SingleOrDefault(p => p.Id == userId);
            if (user == null)
                throw new ArgumentException("Invalid id");
            var addres = _context.Addresses.SingleOrDefault(p => p.Id == user.AddressId);
            return addres;
        }

        /// <see cref="IAddressService.UpdateAddress(int, AddressViewModel)"/>
        /// <exception cref="ArgumentException"/>
        public async Task UpdateAddress(int userId, AddressViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentException();

            var user = _context.Users.Single(u => u.Id == userId);
            var address = _context.Addresses.Single(p => p.Id == user.AddressId);

            address.City = viewModel.City;
            address.PostalCode = viewModel.PostalCode;
            address.Street = viewModel.Street;
            address.HouseNumber = viewModel.HouseNumber;

            await _context.SaveChangesAsync();
        }
    }
}
