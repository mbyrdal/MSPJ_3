using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class Guest
    {
        public string? Email { get; set; } 

        public Guest() { }

        public Guest(string email)
        {
            Email = email;
        }
    }
}
