using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class Guest
    {
        [Required]
        public string Email { get; set; } 

        public Guest(string email)
        {
            Email = email;
        }
    }
}
