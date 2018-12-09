using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Backend.Models;

namespace MVC.Backend.ViewModels
{
    public class FileRequestViewModel
    {
        public string ProductId { get; set; }
        public string Base64 { get; set; }
        public string FileName { get; set; }
    }

    public class FileResultViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Base64 { get; set; }
        public string CreatedAt { get; set; }

        public FileResultViewModel()
        { 
        }

        public FileResultViewModel(ProductFile file)
        {
            Id = file.Id;
            FileName = file.FileName;
            CreatedAt = file.CreatedAt.ToString("g");
        }
    }

    public class FileContent
    {
        public byte[] Bytes{ get; set; }
        public string FileType { get; set; }

        public FileContent(byte[] bytes, string fileType)
        {
            Bytes = bytes;
            FileType = fileType;
        }
    }
}
