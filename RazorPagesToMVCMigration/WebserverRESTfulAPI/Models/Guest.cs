namespace ServiceAPI.Models
{
    public class Guest
    {
        public string Email { get; set; } = string.Empty;
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
