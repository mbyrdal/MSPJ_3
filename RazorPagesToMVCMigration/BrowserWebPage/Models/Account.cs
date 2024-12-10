namespace BrowserWebPage.Models
{
    //Account exist for making accounts, they are not saved in the database but with cookies (for now)
    public class Account
    {
        public int ID { get; set; }
        public string? Email { get; set; } 
        public string? Password { get; set; } 

        public Account(int iD)
        {
            ID = iD;
        }

        public Account(string? email, string? password)
        {
            Email = email;
            Password = password;
        }
    }
}
