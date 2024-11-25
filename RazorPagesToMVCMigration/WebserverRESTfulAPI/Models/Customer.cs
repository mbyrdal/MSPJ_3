using ServiceAPI.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; } = string.Empty;

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

        public Customer() { }

        public Customer(int iD, string email, string firstName, string lastName, string address, string phoneNum)
        {
            ID = iD;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNum = phoneNum;
        }
    }
}
