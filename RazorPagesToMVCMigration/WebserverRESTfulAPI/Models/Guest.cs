using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class Guest
    {
        [Required]
        public string Email { get; set; } 
        public Guest()
        {
            Email = "default@email.com";
        }
        public Guest(string email, int listID)
        {
            Email = email;
        }
    }
}
