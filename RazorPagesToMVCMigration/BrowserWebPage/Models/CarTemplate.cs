using System.ComponentModel.DataAnnotations;

namespace ClientWeb.Models
{
    public class CarTemplate
    {
        [Key]
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

        public CarTemplate()
        {

        }

        public CarTemplate(int ID, string brand, string model, string carType)
        {
            this.ID = ID;
            Brand = brand;
            Model = model;
            CarType = carType;
        }
    }
}
