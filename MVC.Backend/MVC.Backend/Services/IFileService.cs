using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Services
{
    public interface IFileService
    {
        string SaveImage(string productId, string base64);
        string SaveThumbnail(string productId, string base64);
    }
}
