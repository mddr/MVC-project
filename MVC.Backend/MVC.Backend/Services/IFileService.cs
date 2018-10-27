using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Backend.Services
{
    public interface IFileService
    {
        string SaveImage(string productId, string b64);
        string GenerateThumbnail(string productId, string viewModelImageBase64);
    }
}
