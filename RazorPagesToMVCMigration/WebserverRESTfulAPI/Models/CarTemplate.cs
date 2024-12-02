using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class CarTemplate
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(30, ErrorMessage = "Brand cannot exceed 30 characters.")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(40, ErrorMessage = "Model cannot exceed 40 characters.")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Car Type is required.")]
        [StringLength(40, ErrorMessage = "Car Type cannot exceed 40 characters.")]
        [Display(Name = "Car Type")]
        public string CarType { get; set; } = string.Empty;

        public CarTemplate() { }

        public CarTemplate(int id, string brand, string model, string carType)
        {
            ID = id;
            Brand = brand;
            Model = model;
            CarType = carType;
        }
    }
}
