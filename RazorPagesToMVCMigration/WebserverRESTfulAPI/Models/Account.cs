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
        
        [JsonIgnore]
        public string? HashPassword { get; set; } // For internal use only.

        // TODO: implement collection of orders for an account (1-to-Many relationship)
        // public ICollection<Order> Orders { get; set; }

        [JsonConstructor]
        public Account(string guestEmail, string fName, string lName, string address, string pnum/*, List<Order> orderList */) : base(guestEmail)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
            PhoneNum = pnum;
            // Orders = orderList;
        }

        public Account(string guestEmail, string fName, string lName, string address, string pnum, string pw/*, List<Order> orderList */) : base(guestEmail)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
            PhoneNum = pnum;
            HashPassword = HashingHelper.HashAccountPassword(pw);
            // Orders = orderList;
        }
    }
}
