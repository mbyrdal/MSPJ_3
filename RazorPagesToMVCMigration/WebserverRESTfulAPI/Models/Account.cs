namespace ServiceAPI.Models
{
    public class Account : Guest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string Password { get; set; }
        public List<string>? ListOfOrders { get; set; }

        public Account()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = "InTheMiddleOfNowhere Street 50";
            PhoneNum = "12345678";
            Email = "default@email.com";
            Password = "123456abcdef!@#¤%_XYZ";
        }

        public Account(string fName, string lName, string address, string pnum, string email, string pw)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
            PhoneNum = pnum;
            Email = email;
            Password = pw;
        }
    }
}
