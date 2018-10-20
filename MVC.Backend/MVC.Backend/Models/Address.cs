using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Backend.Models
{
    [NotMapped]
    public class Address
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }

        public override string ToString()
        {
            var list = new List<string> {PostalCode, City, Street, HouseNumber};
            return string.Join(";", list);
        }
    }
}
