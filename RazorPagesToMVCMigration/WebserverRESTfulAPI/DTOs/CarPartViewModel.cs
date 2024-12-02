using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.DTOs
{
    public class CarPartViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string Notes { get; set; } = string.Empty;

        public CarPartViewModel() { }

        public CarPartViewModel(int id, int carId, string name, string notes)
        {
            ID = id;
            CarID = carId;
            Name = name;
            Notes = notes;
        }
    }
}
