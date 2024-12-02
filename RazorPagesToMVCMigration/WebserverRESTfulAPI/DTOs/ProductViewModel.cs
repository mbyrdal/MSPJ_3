using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.DTOs
{
    public class ProductViewModel
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public int CarPartID { get; set; }

        [JsonIgnore]
        public int CarID { get; set; }

        [Required(ErrorMessage = "OEM is required.")]
        [StringLength(40, ErrorMessage = "OEM cannot exceed 40 characters.")]
        public string OEM { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Date Available is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Available")]
        public DateTime DateAvailable { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        [StringLength(500, ErrorMessage = "Condition cannot exceed 500 characters.")]
        public string Condition { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Item Description cannot exceed 1000 characters.")]
        public string? ItemDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Item availability is required.")]
        public bool ItemAvailable { get; set; }

        public ProductViewModel() { }

        public ProductViewModel(int id, int carPartID, int carID, string oem, decimal price, DateTime dateAvailable, string condition, string? itemDescription, bool itemAvailable)
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
