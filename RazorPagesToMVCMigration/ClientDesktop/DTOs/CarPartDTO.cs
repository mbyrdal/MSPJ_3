using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientDesktop.DTOs
{
    public class CarPartDTO
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CarID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Notes { get; set; } = string.Empty;

        // Navigation Property for CarModel
        // [ForeignKey("CarModelID")]
        // public CarModel CarModel { get; set; }

        public CarPartDTO() { }

        public CarPartDTO(int ID, int carID, string name, string notes)
        {
            this.ID = ID;
            CarID = carID;
            Name = name;
            Notes = notes;
        }
    }
}
