using ClientWeb.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientWeb.Models
{
    public class Customer
    {
        [Key]
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

        public Customer() { }

        public Customer(int ID, string firstName, string lastName, string address, string phoneNum, string email)
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
