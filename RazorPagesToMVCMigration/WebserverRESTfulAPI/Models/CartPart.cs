using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class CartPart
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CarModelID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Notes { get; set; } = string.Empty;

        // Navigation Property for CarModel
        // [ForeignKey("CarModelID")]
        // public CarModel CarModel { get; set; }

        public CartPart() { }

        public CartPart(int ID, int carModelID, string name, string notes)
        {
            this.ID = ID;
            CarModelID = carModelID;
            Name = name;
            Notes = notes;
        }
    }
}
