using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientDesktop.DTOs
{
    // DTO VERSION
    public class CustomerDTO
    {
        [Key]
        [JsonIgnore]
        public int ID { get; set; }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string PhoneNum { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Email { get; set; } = string.Empty;

        public CustomerDTO() { }

        public CustomerDTO(int ID, string firstName, string lastName, string address, string phoneNum, string email)
        {
            this.ID = ID;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNum = phoneNum;
            Email = email;
        }
    }
}
