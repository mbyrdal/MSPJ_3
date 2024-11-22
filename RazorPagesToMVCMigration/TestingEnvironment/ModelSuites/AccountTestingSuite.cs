using ServiceAPI.Models;
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
        /// This test checks the validity of an Account object created using the default constructor.
        /// </summary>
        [Fact]
        public void Account_DefaultConstructor_CreatesUserUsingDefaultConstructor()
        {
            // Arrange
            string defaultFirstName = string.Empty;
            string defaultLastName = string.Empty;
            string defaultAddress = "InTheMiddleOfNowhere Street 50";
            string defaultPhoneNum = "12345678";
            string defaultEmail = "default@email.com";
            string defaultPlaintextPassword = "123456abcdef!@#¤%_XYZ";

            // Act
            Account myDefaultAccount = new Account(); // Supposedly applies default values
            bool resultPassword = myDefaultAccount.VerifyAccountPassword(defaultPlaintextPassword);

            // Assert
            Assert.IsType<Account>(myDefaultAccount); // Expected type is User. User inherits ID from Customer
            Assert.Equal(defaultFirstName, myDefaultAccount.FirstName); // Expected value is string.Empty ("")
            Assert.Equal(defaultLastName, myDefaultAccount.FirstName); // Expected value is string.Empty ("")
            Assert.Equal(defaultAddress, myDefaultAccount.Address); // Expected value is "InTheMiddleOfNowhere Street 50"
            Assert.Equal(defaultPhoneNum, myDefaultAccount.PhoneNum); // Expected value is 12345678
            Assert.Equal(defaultEmail, myDefaultAccount.Email); // Expected value is "default@gmail.com"
            Assert.True(resultPassword); // Expected value is True for "123456abcdef!@#¤%_XYZ"
        }

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
            string password = "09JD08djuq980JUD80qdjk+980D";

            // Act
            Account myCustomUser = new Account(firstName, lastName, address, phoneNum, email, password);

            // Assert
            Assert.IsType<Account>(myCustomUser);
            Assert.Equal(firstName, myCustomUser.FirstName); // Expected value is "Hans"
            Assert.Equal(lastName, myCustomUser.LastName); // Expected value is "Hansen"
            Assert.Equal(address, myCustomUser.Address); // Expected value is "Danmarksgade 55, 9000 Aalborg"
            Assert.Equal(phoneNum, myCustomUser.PhoneNum); // Expected value is 224466888
            Assert.Equal(email, myCustomUser.Email); // Expected value is "student1234@ucn.dk"
            Assert.Equal(password, myCustomUser.Password); // Expected value is "09JD08djuq980JUD80qdjk+980D"

        }
    }
}
