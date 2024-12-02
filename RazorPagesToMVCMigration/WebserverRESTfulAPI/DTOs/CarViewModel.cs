using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.DTOs
{
    public class CarViewModel
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public int CarTemplateID { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "VIN Number cannot exceed 25 characters.")]
        public string VINNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Production Year")]
        public DateTime ProductionYear { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number.")]
        public int Mileage { get; set; }

        public CarViewModel() { }

        public CarViewModel(int id, int carTemplateID, string vinNumber, DateTime productionYear, int mileage)
        {
            ID = id;
            CarTemplateID = carTemplateID;
            VINNumber = vinNumber;
            ProductionYear = productionYear;
            Mileage = mileage;
        }
    }
}
