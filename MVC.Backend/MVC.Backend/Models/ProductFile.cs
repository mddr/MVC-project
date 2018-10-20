using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVC.Backend.Models
{
    public class ProductFile
    {
        [Key] public int Id { get; set; }
        [Required] public string FilePath { get; set; }
        [Required] public DateTime CreatedAt => DateTime.Now;

        [ForeignKey("Products")] public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}