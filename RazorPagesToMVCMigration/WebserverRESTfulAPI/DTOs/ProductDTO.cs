using ServiceAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServiceAPI.DTOs
{
    public class ProductDTO
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public int CarPartID { get; set; }

        [JsonIgnore]
        public int CarID { get; set; }

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

        public string? ItemDescription { get; set; } = string.Empty;

        [Required]
        public bool ItemAvailable { get; set; }

        public ProductDTO() { }

        public ProductDTO(int ID, int carPartID, int carID, string oem, decimal price, DateTime dt, string cond, string itemDesc, bool itemAvailable)
        {
            this.ID = ID;
            CarPartID = carPartID;
            CarID = carID;
            OEM = oem;
            Price = price;
            DateAvailable = dt;
            Condition = cond;
            ItemDescription = itemDesc;
            ItemAvailable = itemAvailable;
        }
    }
}
