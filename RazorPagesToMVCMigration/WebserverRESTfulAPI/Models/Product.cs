using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.Models
{
    public class Product
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [Required(ErrorMessage = "Car Part ID is required.")]
        public int CarPartID { get; set; }

        [Required(ErrorMessage = "Car ID is required.")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "OEM is required.")]
        [StringLength(40, ErrorMessage = "OEM cannot exceed 40 characters.")]
        public string OEM { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Date Available is required.")]
        [DataType(DataType.Date)]
        public DateTime DateAvailable { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        [StringLength(500, ErrorMessage = "Condition cannot exceed 500 characters.")]
        public string Condition { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Item Description cannot exceed 1000 characters.")]
        public string? ItemDescription { get; set; } = string.Empty;

        [Required]
        public bool ItemAvailable { get; set; }

        public Product() { }

        public Product(int id, int carPartID, int carID, string oem, decimal price, DateTime dateAvailable, string condition, string? itemDescription, bool itemAvailable)
        {
            ID = id;
            CarPartID = carPartID;
            CarID = carID;
            OEM = oem;
            Price = price;
            DateAvailable = dateAvailable;
            Condition = condition;
            ItemDescription = itemDescription;
            ItemAvailable = itemAvailable;
        }
    }
}
