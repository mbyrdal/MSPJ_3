using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientWeb.Models
{
    public class Car
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

        public Car() { }

        public Car(int ID, int carTemplateID, string vin, DateTime py, int mileage)
        {
            this.ID = ID;
            CarTemplateID = carTemplateID;
            VINNumber = vin;
            ProductionYear = py;
            Mileage = mileage;
        }
    }
}
