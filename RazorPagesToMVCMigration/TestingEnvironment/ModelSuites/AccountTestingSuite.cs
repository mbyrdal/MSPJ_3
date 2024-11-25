using ServiceAPI.Models;
using ServiceAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPJ.TestingEnvironment.ModelSuites
{
    public class AccountTestingSuite
    {
        /// <summary>
        /// This test checks the validity of an Account object created using a signature constructor.
        /// </summary>
        [Fact]
        public void Account_CustomConstructor_CreatesUserUsingCustomConstructor()
        {
            // Arrange
            string firstName = "Hans";
            string lastName = "Hansen";
            string address = "Danmarksgade 55, 9000 Aalborg";
            string phoneNum = "22446688";
            string email = "student1234@ucn.dk";
            string plainTextPassword = "09JD08djuq980JUD80qdjk+980D";

            // Act
            Account myCustomAccount = new Account(email, firstName, lastName, address, phoneNum, plainTextPassword);
            bool doPasswordsMatch = HashingHelper.VerifyAccountPassword(plainTextPassword, myCustomAccount.HashPassword);

            // Assert
            Assert.IsType<Account>(myCustomAccount);
            Assert.Equal(email, myCustomAccount.Email); // Expected value is "student1234@ucn.dk"
            Assert.Equal(firstName, myCustomAccount.FirstName); // Expected value is "Hans"
            Assert.Equal(lastName, myCustomAccount.LastName); // Expected value is "Hansen"
            Assert.Equal(address, myCustomAccount.Address); // Expected value is "Danmarksgade 55, 9000 Aalborg"
            Assert.Equal(phoneNum, myCustomAccount.PhoneNum); // Expected value is 224466888
            Assert.True(doPasswordsMatch); // Expected value is True for "09JD08djuq980JUD80qdjk+980D"
        }
    }
}
