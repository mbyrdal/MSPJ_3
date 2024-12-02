using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(40, ErrorMessage = "First Name cannot exceed 40 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(40, ErrorMessage = "Last Name cannot exceed 40 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")]
        [RegularExpression(@"^\+?[0-9\s\-]+$", ErrorMessage = "Invalid Phone Number format.")]
        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string Email { get; set; } = string.Empty;

        public Customer() { }

        public Customer(int id, string firstName, string lastName, string address, string phoneNum, string email)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNum = phoneNum;
            Email = email;
        }
    }
}
