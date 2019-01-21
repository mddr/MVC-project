using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using Microsoft.Extensions.Options;
using MimeTypes;
using MVC.Backend.Helpers;
using MVC.Backend.ViewModels;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace MVC.Backend.Services
{
    /// <see cref="IFileService"/>
    public class FileService : IFileService
    {
        private readonly IOptions<DirectorySettings> _directorySettings;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="directorySettings">Opcje katalogów</param>
        public FileService(IOptions<DirectorySettings> directorySettings)
        {
            _directorySettings = directorySettings;
        }

        /// <see cref="IFileService.GetFileContent(string)"/>
        public FileContent GetFileContent(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var extension = Path.GetExtension(filePath);
            var fileType = MimeTypeMap.GetMimeType(extension);
            var content = new FileContent(bytes, fileType);
            return content;
        }

        /// <see cref="IFileService.SaveFile(string, string, string)"/>
        public string SaveFile(string productId, string base64, string fileName)
        {
            var mimeType = GetMime(base64);
            var extension = MimeTypeMap.GetExtension(mimeType);
            var filePath = GenerateFilePath(productId, extension, _directorySettings.Value.Files, fileName);

            var bytes = Convert.FromBase64String(GetBase64(base64));
            File.WriteAllBytes(filePath, bytes);

            return filePath;
        }
        
        /// <see cref="IFileService.SaveImage(string, string)"/>
        public string SaveImage(string productId, string base64)
        {
            var filePath = GenerateFilePath(productId, ".jpg", _directorySettings.Value.Images);

            var size = new Size(900, 900);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

        /// <see cref="IFileService.SaveThumbnail(string, string)(string)"/>
        public string SaveThumbnail(string productId, string base64)
        {
            var filePath = GenerateThumbnailFilePath(productId, ".jpg", _directorySettings.Value.Images);
            var size = new Size (200, 200);
            var resizedImage = ResizeImage(base64, size);
            File.WriteAllBytes(filePath, resizedImage);

            return filePath;
        }

        /// <see cref="IFileService.DeleteFile(string)"/>
        /// <exception cref="ArgumentException"/>
        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File {filePath} doesn't exist");
            File.Delete(filePath);
        }

        /// <summary>
        /// Skaluje obrazek
        /// </summary>
        /// <param name="base64">Obrazek jako base64</param>
        /// <param name="size">Docelowy rozmair</param>
        /// <returns>Miniaturka</returns>
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

        /// <summary>
        /// Generuje ścieżke pliku
        /// </summary>
        /// <param name="productId">Id produktu</param>
        /// <param name="extension">Rozszerzenie</param>
        /// <param name="basePath">Ścieżka bazowa</param>
        /// <returns>Ścieżka pliku</returns>
        private static string GenerateFilePath(string productId, string extension, string basePath)
        {
            var sb = new StringBuilder();
            sb.Append(basePath);
            sb.Append(productId);
            sb.Append(extension);
            return sb.ToString();
        }

        /// <summary>
        /// Generuje ścieżke pliku
        /// </summary>
        /// <param name="productId">Id pliku</param>
        /// <param name="extension">Rozszerzenie</param>
        /// <param name="basePath">Ścieżka bazowa</param>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns>Ścieżka pliku</returns>
		private static string GenerateFilePath(string productId, string extension, string basePath, string fileName)
		{
			var sb = new StringBuilder();
			sb.Append(basePath);
			sb.Append(productId);
			sb.Append(Guid.NewGuid().ToString());
			sb.Append(fileName);
			sb.Append(extension);
			return sb.ToString();
		}

        /// <summary>
        /// Generuje ścieżke miniatruki
        /// </summary>
        /// <param name="productId">Id pliku</param>
        /// <param name="extension">Rozszerzenie</param>
        /// <param name="basePath">Ścieżka bazowa</param>
        /// <returns>Ścieżka miniaturki</returns>
		private static string GenerateThumbnailFilePath(string productId, string extension, string basePath)
        {
            var sb = new StringBuilder();
            sb.Append(basePath);
            sb.Append(productId);
            sb.Append("_thumb");
            sb.Append(extension);
            return sb.ToString();
        }

        /// <summary>
        /// Wyciąga typ mime
        /// </summary>
        /// <param name="base64">Plik jako base64</param>
        /// <returns>String reprezentujący typ mime pliku</returns>
        private string GetMime(string base64)
        {
            var split = base64.Split(';');
            return split[0].Replace("data:", string.Empty);
        }

        /// <summary>
        /// Ignoruje przedrostek identyfikujący base64
        /// </summary>
        /// <param name="base64">base64 z identyfikującym przedrostkiem</param>
        /// <returns>Samo base64</returns>
        private static string GetBase64(string base64)
        {
            var split = base64.Split(',');
            return split.Length == 1 ? base64 : split[1];
        }
    }
}
