using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientWeb.DTOs
{
    public class CarPartDTO
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        [JsonIgnore]
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
