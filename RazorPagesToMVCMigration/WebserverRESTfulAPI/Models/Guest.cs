namespace ServiceAPI.Models
{
    public class Customer
    {
        public string Email { get; set; } = string.Empty;
        public int ListID { get; set; }

        public Customer()
        {
            Email = "default@email.com";
            ListID = 0;
        }

        public Customer(string email, int listID)
        {
            Email = email;
            ListID = listID;
        }
    }
}
