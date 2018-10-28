using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.Helpers
{
    public static class Extensions
    {
        public static string GetBase64(this Product product)
        {
            try
            {
                var bytes = File.ReadAllBytes(product.ThumbnailPath);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
