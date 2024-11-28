using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int SaleID { get; set; }

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

        public DateTime DateSold { get; set; }

        public bool IsAvailable { get; set; }

        public ProductViewModel() { }

        public ProductViewModel(int ID, int cartPartID, int saleID, string oem, decimal price, DateTime dt, string cond, string itemDesc, DateTime dateSold, bool isAvailable)
        {
            this.ID = ID;
            CarPartID = cartPartID;
            SaleID = saleID;
            OEM = oem;
            Price = price;
            DateAvailable = dt;
            Condition = cond;
            ItemDescription = itemDesc;
            DateSold = dateSold;
            IsAvailable = isAvailable;
        }
    }
}
