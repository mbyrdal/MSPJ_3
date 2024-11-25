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
        public string Name { get; set; }

        [Required]
        public string Notes { get; set; }

        // Navigation Property for CarModel
        // [ForeignKey("CarModelID")]
        // public CarModel CarModel { get; set; }
    }
}
