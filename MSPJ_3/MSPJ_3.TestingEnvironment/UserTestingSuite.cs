using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPJ_3.TestingEnvironment
{
    public class UserTestingSuite
    {
        /// <summary>
        /// This test checks the validity of a User object created using the default implementation constructor.
        /// </summary>
        [Fact]
        public void Test_User_CreateUsingDefaultConstructor()
        {
            // Arrange
            // string defaultFirstName = String.Empty;
            // string defaultLastName = String.Empty;
            // string defaultAddress = "InTheMiddleOfNowhere Street 50";
            // int defaultPhoneNumber = 12345678;
            // string defaultEmail = "default@email.com";
            // string defaultPassword = "123456abcdef!@#¤%_XYZ";

            // Act
            // User myDefaultUser = new User(); // Supposedly applies default values... to be checked.

            // Assert
            // Assert.IsType<User>(myDefaultUser); // Expected type is User. User inherits ID from Customer.
            // Assert.Equal(defaultFirstName, myDefaultUser.FirstName); // Expected value is String.Empty ("")
            // Assert.Equal(defaultLastName, myDefaultUser.FirstName); // Expected value is String.Empty ("")
            // Assert.Equal(defaultAddress, myDefaultUser.Address); // Expected value is "InTheMiddleOfNowhere Street 50"
            // Assert.Equal(defaultPhoneNumber, myDefaultUser.PhoneNumber); // Expected value is 12345678
            // Assert.Equal(defaultEmail, myDefaultUser.Email) // Expected value is "default@gmail.com"
            // Assert.Equal(defaultPassword, myDefaultUser.Password) // Expected value is "123456abcdef!@#¤%_XYZ"
        }

        /// <summary>
        /// This test checks the validity of a User object created using a parameterized constructor.
        /// </summary>
        [Fact]
        public void Test_User_CreateUsingParameters()
        {
            // Arrange
            // string firstName = "Hans";
            // string lastName = "Hansen";
            // string address = "Danmarksgade 55, 9000 Aalborg";
            // int phoneNum = 22446688;
            // string email = "student1234@ucn.dk";
            // password = "09JD08djuq980JUD80qdjk+980D";

            // Act
            // User myCustomUser = new User(firstName, lastName, address, phoneNum, email, password);

            // Assert
            // Assert.IsType<User>(myCustomUser);
            // Assert.Equal(firstName, myCustomUser.FirstName); // Expected value is "Hans"
            // Assert.Equal(lLastName, myCustomUser.FirstName); // Expected value is "Hansen"
            // Assert.Equal(address, myCustomUser.Address); // Expected value is "Danmarksgade 55, 9000 Aalborg"
            // Assert.Equal(phoneNum, myCustomUser.PhoneNumber); // Expected value is 224466888
            // Assert.Equal(email, myCustomUser.Email) // Expected value is "student1234@ucn.dk"
            // Assert.Equal(password, myCustomUser.Password) // Expected value is "09JD08djuq980JUD80qdjk+980D"

        }
    }
}
