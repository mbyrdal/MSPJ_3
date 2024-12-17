namespace ClientWeb.Utilities
{
    public static class HashingHelper
    {
        /// <summary>
        /// This method hashes a plaintext password using the bcrypt algorithm.
        /// </summary>
        /// <param name="plainTextPassword">The plaintext password to be hashed, provided as input parameter.</param>
        /// <returns>The hashed password.</returns>
        public static string HashAccountPassword(string plainTextPassword)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword))
            {
                throw new ArgumentException("Password cannot be either null or have whitespace.", nameof(plainTextPassword));
            }

            // Generate a salt and hash password.
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        /// <summary>
        /// This method verifies a plaintext password against a hashed password.
        /// </summary>
        /// <param name="plainTextPassword">The plaintext password to be hashed, provided as input parameter.</param>
        /// <returns>True or False, depending on whether there is a match between the password and hash.</returns>
        public static bool VerifyAccountPassword(string plainTextPassword, string hashPassword)
        {
            if (string.IsNullOrWhiteSpace(plainTextPassword))
            {
                throw new ArgumentException("Password cannot be either null or have whitespace.", nameof(plainTextPassword));
            }

            if (string.IsNullOrWhiteSpace(hashPassword))
            {
                throw new ArgumentException("Hashed password cannot be either null or have whitespace.", nameof(hashPassword));
            }

            // Compare the plain text password to the hash
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashPassword);
        }

    }
}
