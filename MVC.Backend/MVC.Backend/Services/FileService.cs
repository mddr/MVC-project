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

namespace MVC.Backend.Services
{
    public class FileService : IFileService
    {
        private readonly IOptions<DirectorySettings> _directorySettings;

        public FileService(IOptions<DirectorySettings> directorySettings)
        {
            _directorySettings = directorySettings;
        }

        public string SaveImage(string productId, string base64)
        {
            var filePath = GenerateFilePath(productId);

            var size = new Size(900, 900);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

        public string SaveThumbnail(string productId, string base64)
        {
            var filePath = GenerateThumbnailFilePath(productId);

            var size = new Size (200, 200);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

    #region helpers

        private byte[] ResizeImage(string base64, Size size)
        {
            var imageBytes = Convert.FromBase64String(GetImage(base64));
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

        private string GenerateFilePath(string productId)
        {
            const string extension = "jpg";

            var sb = new StringBuilder();
            sb.Append(_directorySettings.Value.Images);
            sb.Append(productId);
            sb.Append(".");
            sb.Append(extension);
            return sb.ToString();
        }

        private string GenerateThumbnailFilePath(string productId)
        {
            const string extension = "jpg";

            var sb = new StringBuilder();
            sb.Append(_directorySettings.Value.Images);
            sb.Append(productId);
            sb.Append("_thumb.");
            sb.Append(extension);
            return sb.ToString();
        }

        private string GetMime(string base64)
        {
            var split = base64.Split(',');
            return split[0];
        }

        private string GetImage(string base64)
        {
            var split = base64.Split(',');
            return split[1];
        }

        #endregion
    }
}
