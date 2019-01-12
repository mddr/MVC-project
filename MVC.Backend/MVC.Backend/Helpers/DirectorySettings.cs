namespace MVC.Backend.Helpers
{
    /// <summary>
    /// Umożliwia pobranie ustawień dotycząchych folderów z appsettings.json
    /// </summary>
    public class DirectorySettings
    {
        /// <summary>
        /// Fodler na obrazki
        /// </summary>
        public string Images { get; set; }
        /// <summary>
        /// Folder na pliki
        /// </summary>
        public string Files { get; set; }
    }
}
