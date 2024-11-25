using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class CarTemplate
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }

        [Required]
        [StringLength(40)]
        public string Model { get; set; }

        [Required]
        [StringLength(40)]
        public string CarType { get; set; }
    }
}
