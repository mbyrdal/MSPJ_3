using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientWeb.DTOs
{
    public class CarDTO
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public int CarTemplateID { get; set; }

        [Required]
        [StringLength(25)]
        public string VINNumber { get; set; } = string.Empty;

        [Required]
        public DateTime ProductionYear { get; set; }

        [Required]
        public int Mileage { get; set; }

        // Navigation Property for CarTemplate
        // [ForeignKey("CarTemplate")]
        // public CarTemplate CarTemplate { get; set; }

        public CarDTO() { }

        public CarDTO(int ID, int carTemplateID, string vin, DateTime py, int mileage)
        {
            this.ID = ID;
            CarTemplateID = carTemplateID;
            VINNumber = vin;
            ProductionYear = py;
            Mileage = mileage;
        }
    }
}
