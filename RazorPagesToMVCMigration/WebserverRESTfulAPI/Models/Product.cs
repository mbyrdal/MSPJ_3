using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.Models
{
    public class Product
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        public int CarPartID { get; set; }

        [Required]
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

        public string ItemDescription { get; set; } = string.Empty;

        public bool ItemAvailable { get; set; }
        public Car? ProductCarOrigin { get; set; }
        public CarPart? ProductCarPartOrigin { get; set; }

        public Product() { }

        public Product(int ID, int cartPartID, int carID, string oem, decimal price, DateTime dt, string cond, string itemDesc, bool isAvailable)
        {
            this.ID = ID;
            CarPartID = cartPartID;
            CarID = carID;
            OEM = oem;
            Price = price;
            DateAvailable = dt;
            Condition = cond;
            ItemDescription = itemDesc;
            ItemAvailable = isAvailable;
        }
    }
}