using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceAPI.Models
{
    public class CarPart
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Car ID is required.")]
        public int CarID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Notes are required.")]
        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string Notes { get; set; } = string.Empty;

        // Navigation Property for Car
        // Uncomment if a relationship is needed in the future
        // [ForeignKey("CarID")]
        // public Car? Car { get; set; }

        public CarPart() { }

        public CarPart(int id, int carId, string name, string notes)
        {
            ID = id;
            CarID = carId;
            Name = name;
            Notes = notes;
        }
    }
}
