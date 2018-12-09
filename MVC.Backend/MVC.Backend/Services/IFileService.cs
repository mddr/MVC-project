using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public interface IFileService
    {
        FileContent GetFileContent(string filePath);
        string SaveFile(string productId, string base64);
        string SaveImage(string productId, string base64);
        string SaveThumbnail(string productId, string base64);
        void DeleteFile(string filePath);
    }
}
