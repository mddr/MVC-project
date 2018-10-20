using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Helpers
{
    public class Enums
    {
        public enum Currency
        {
            PLN = 0,
            EUR = 1,
            USD = 2
        }

        public enum Roles
        {
            User = 0,
            Admin = 1
        }
    }
}
