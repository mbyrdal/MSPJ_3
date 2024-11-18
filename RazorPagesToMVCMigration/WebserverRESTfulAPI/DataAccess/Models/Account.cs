namespace ServiceAPI.DatabaseAccess.Models
{
    public class Account
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string>? ListOfOrders { get; set; }
    }
}
