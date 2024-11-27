using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.DTOs
{
    public class CarTemplateViewModel
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string CarType { get; set; } = string.Empty;

        public CarTemplateViewModel() { }

        public CarTemplateViewModel(int ID, string brand, string model, string carType)
        {
            this.ID = ID;
            Brand = brand;
            Model = model;
            CarType = carType;
        }
    }
}
