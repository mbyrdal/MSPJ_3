using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CarPartId { get; set; }

        [Required]
        public int SaleId { get; set; }

        [Required]
        [StringLength(40)]
        public string OEM { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime DateAvailable { get; set; }

        [Required]
        [StringLength(500)]
        public string Condition { get; set; } = string.Empty;

        public string ItemDescription { get; set; }

        public Product() { }
    }
}
