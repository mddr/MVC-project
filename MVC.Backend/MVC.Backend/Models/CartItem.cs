using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Backend.Models
{
    public class CartItem
    {
        public string Id { get; set; }

        [ForeignKey("Users")] public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Products")] public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required] public int ProductAmount { get; set; }
        [Required] public bool IsValid { get; set; }
    }
}