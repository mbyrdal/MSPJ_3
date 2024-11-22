using ServiceAPI.DatabaseAccess.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceAPI.Models
{
    public class Account : Guest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string PhoneNum { get; set; }
        
        [Required]
        [JsonIgnore]
        public string HashPassword { get; set; } // For internal use only.

        // TODO: implement collection of orders for an account (1-to-Many relationship)
        // public ICollection<Order> Orders { get; set; }

        public Account()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = "InTheMiddleOfNowhere Street 50";
            PhoneNum = "12345678";
            Email = "default@email.com";
            HashPassword = HashingHelper.HashAccountPassword("123456abcdef!@#¤%_XYZ");
            // Orders = new List<Order>();
        }

        public Account(string fName, string lName, string address, string pnum, string email, string pw/*, List<Order> orderList */)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
            PhoneNum = pnum;
            Email = email;
            HashPassword = HashingHelper.HashAccountPassword(pw);
            // Orders = orderList;
        }
    }
}
