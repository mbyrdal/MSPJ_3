namespace ServiceAPI.Models
{
    public class Guest
    {
        public string Email { get; set; } = string.Empty;
        public int OrderID { get; set; }

        public Guest()
        {
            Email = "default@email.com";
            OrderID = 0;
        }

        public Guest(string email, int orderID)
        {
            Email = email;
            OrderID = orderID;
        }
    }
}
