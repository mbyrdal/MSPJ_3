namespace ServiceAPI.Models
{
    public class Account : Guest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        private string _passwordHash;
        public List<string>? ListOfOrders { get; set; }
        /// <summary>
        /// This method verifies a plaintext password against a hashed password.
        /// </summary>
        /// <param name="plainTextPassword">The plaintext password to be hashed, provided as input parameter.</param>
        /// <returns>True or False, depending on whether there is a match between the password and hash.</returns>
        public bool VerifyAccountPassword(string plainTextPassword)
        {
            // Compare the plain text password to the hash
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, _passwordHash);
        }
        /// <summary>
        /// This method hashes a plaintext password using the bcrypt algorithm.
        /// </summary>
        /// <param name="plainTextPassword">The plaintext password to be hashed, provided as input parameter.</param>
        /// <returns>The hashed password.</returns>
        private string HashAccountPassword(string plainTextPassword)
        {
            // Generate a salt and hash password.
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }
        public Account()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = "InTheMiddleOfNowhere Street 50";
            PhoneNum = "12345678";
            Email = "default@email.com";
            _passwordHash = HashAccountPassword("123456abcdef!@#¤%_XYZ");
        }

        public Account(string fName, string lName, string address, string pnum, string email, string pw)
        {
            FirstName = fName;
            LastName = lName;
            Address = address;
            PhoneNum = pnum;
            Email = email;
            _passwordHash = HashAccountPassword(pw);
        }
    }
}
