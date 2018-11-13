using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Helpers;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class SignupViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? AddressId;
    }
}
