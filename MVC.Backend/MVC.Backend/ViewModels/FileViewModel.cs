using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    /// <summary>
    /// Dane uploadowanego pliku
    /// </summary>
    public class FileRequestViewModel
    {
        /// <summary>
        /// Id powiązanego produktu
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Plik w formie base64
        /// </summary>
        public string Base64 { get; set; }
        /// <summary>
        /// Nazwa pliku
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Opis pliku
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Dane pliku związanego z produktem
    /// </summary>
    public class FileResultViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nazwa pliku
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Typ pliku
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// Zawartość pliku w base64
        /// </summary>
        public string Base64 { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        public string CreatedAt { get; set; }
        /// <summary>
        /// Opis
        /// </summary>
        public string Description { get; set; }

        public FileResultViewModel()
        { 
        }

        public FileResultViewModel(ProductFile file)
        {
            Id = file.Id;
            FileName = file.FileName;
            CreatedAt = file.CreatedAt.ToString("g");
            Description = file.Description;
        }
    }

    /// <summary>
    /// Zawartość odczytanego pliku w bajtach oraz jego typ
    /// </summary>
    public class FileContent
    {
        /// <summary>
        /// Zawartość pliku
        /// </summary>
        public byte[] Bytes{ get; set; }
        /// <summary>
        /// Typ pliku
        /// </summary>
        public string FileType { get; set; }

        public FileContent(byte[] bytes, string fileType)
        {
            Bytes = bytes;
            FileType = fileType;
        }
    }
}
