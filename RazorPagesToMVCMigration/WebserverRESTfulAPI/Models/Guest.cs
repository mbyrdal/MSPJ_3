namespace ServiceAPI.Models
{
    public class Guest
    {
        public string Email { get; set; } = string.Empty;
        public int ListID { get; set; }

        public Guest()
        {
            Email = "default@email.com";
            ListID = 0;
        }

        public Guest(string email, int listID)
        {
            Email = email;
            ListID = listID;
        }
    }
}
