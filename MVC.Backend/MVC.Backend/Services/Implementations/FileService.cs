using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using MVC.Backend.Helpers;
using MimeTypes;
using MVC.Backend.Models;
using MVC.Backend.ViewModels;

namespace MVC.Backend.Services
{
    public class FileService : IFileService
    {
        private readonly IOptions<DirectorySettings> _directorySettings;

        public FileService(IOptions<DirectorySettings> directorySettings)
        {
            _directorySettings = directorySettings;
        }

        public FileContent GetFileContent(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var extension = Path.GetExtension(filePath);
            var fileType = MimeTypeMap.GetMimeType(extension);
            var content = new FileContent(bytes, fileType);
            return content;
        }

        public string SaveFile(string productId, string base64)
        {
            var mimeType = GetMime(base64);
            var extension = MimeTypeMap.GetExtension(mimeType);
            var filePath = GenerateFilePath(productId, extension, _directorySettings.Value.Files);

            var bytes = Convert.FromBase64String(GetBase64(base64));
            File.WriteAllBytes(filePath, bytes);

            return filePath;
        }

        public string SaveImage(string productId, string base64)
        {
            var filePath = GenerateFilePath(productId, ".jpg", _directorySettings.Value.Images);

            var size = new Size(900, 900);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

        public string SaveThumbnail(string productId, string base64)
        {
            var filePath = GenerateThumbnailFilePath(productId, ".jpg", _directorySettings.Value.Images);
            var size = new Size (200, 200);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File {filePath} doesn't exist");
            File.Delete(filePath);
        }

        private static byte[] ResizeImage(string base64, Size size)
        {
            var imageBytes = Convert.FromBase64String(GetBase64(base64));
            var format = new JpegFormat {Quality = 80};

            using (var inStream = new MemoryStream(imageBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory(true))
                    {
                        var resizeLayer = new ResizeLayer(size, ResizeMode.Max);
                        imageFactory.Load(inStream)
                            .Resize(resizeLayer)
                            .Format(format)
                            .Save(outStream);
                    }

                    return outStream.ToArray();
                }
            }
        }

        private static string GenerateFilePath(string productId, string extension, string basePath)
        {
            var sb = new StringBuilder();
            sb.Append(basePath);
            sb.Append(productId);
            sb.Append(extension);
            return sb.ToString();
        }

        private static string GenerateThumbnailFilePath(string productId, string extension, string basePath)
        {
            var sb = new StringBuilder();
            sb.Append(basePath);
            sb.Append(productId);
            sb.Append("_thumb");
            sb.Append(extension);
            return sb.ToString();
        }

        private string GetMime(string base64)
        {
            var split = base64.Split(';');
            return split[0].Replace("data:", string.Empty);
        }

        private static string GetBase64(string base64)
        {
            var split = base64.Split(',');
            return split.Length == 1 ? base64 : split[1];
        }
    }
}
