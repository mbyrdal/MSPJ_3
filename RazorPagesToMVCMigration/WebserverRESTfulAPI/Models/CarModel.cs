using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Models
{
    public class CarModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
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

        public CarModel() { }

        public CarModel(int ID, int carTemplateID, string vin, DateTime py, int mileage)
        {
            this.ID = ID;
            CarTemplateID = carTemplateID;
            VINNumber = vin;
            ProductionYear = py;
            Mileage = mileage;
        }
    }
}
