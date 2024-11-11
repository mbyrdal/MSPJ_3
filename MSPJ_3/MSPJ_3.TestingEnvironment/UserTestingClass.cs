using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPJ_3.TestingEnvironment
{
    public class UserTestingClass
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


            // Act


            // Assert

        }
    }
}
