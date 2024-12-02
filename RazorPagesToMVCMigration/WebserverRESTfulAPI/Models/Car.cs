using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Car Template ID is required.")]
        public int CarTemplateID { get; set; }

        [Required(ErrorMessage = "VIN Number is required.")]
        [StringLength(25, ErrorMessage = "VIN Number cannot exceed 25 characters.")]
        public string VINNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Production Year is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Production Year")]
        public DateTime ProductionYear { get; set; }

        [Required(ErrorMessage = "Mileage is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage must be a non-negative number.")]
        public int Mileage { get; set; }

        // Navigation Property for CarTemplate
        // Uncomment if a relationship with `CarTemplate` is required in EF Core
        // [ForeignKey("CarTemplateID")]
        // public CarTemplate? CarTemplate { get; set; }

        public Car() { }

        public Car(int id, int carTemplateId, string vinNumber, DateTime productionYear, int mileage)
        {
            ID = id;
            CarTemplateID = carTemplateId;
            VINNumber = vinNumber;
            ProductionYear = productionYear;
            Mileage = mileage;
        }
    }
}
