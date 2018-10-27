using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using MVC.Backend.Helpers;

namespace MVC.Backend.Services
{
    public class FileService : IFileService
    {
        private readonly IOptions<DirectorySettings> _directorySettings;

        public FileService(IOptions<DirectorySettings> directorySettings)
        {
            _directorySettings = directorySettings;
        }

        public string SaveImage(string productId, string b64)
        {
            var split = b64.Split(',');
            var mime = split[0];
            var image = split[1];

            var filePath = GenerateFilePath(productId, mime);

            File.WriteAllBytes(filePath, Convert.FromBase64String(image));
            return filePath;
        }

        public string GenerateThumbnail(string productId, string viewModelImageBase64)
        {
            throw new NotImplementedException();
        }

        private string GenerateFilePath(string productId, string mime)
        {
            var extension = mime.Split(';')[0].Split('/')[1];

            var sb = new StringBuilder();
            sb.Append(_directorySettings.Value.Images);
            sb.Append(productId);
            sb.Append(".");
            sb.Append(extension);
            return sb.ToString();
        }
    }
}
