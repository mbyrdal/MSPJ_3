namespace MSPJ_3.TestingEnvironment
{   /// Alt skal ændres inde i denne fil, det er kun kopieret indtil videre
    // Til Shemon: Vi arbejder med Customers, og de har kun et unikt ID som attribute.
    // Lav evt. nogle tests og edge cases, hvor:

    //          - Customer object creation.
    //          - Append ID to customer.
    //          - Unique ID generation (Vigtig! Omfatter kobling + querying DB (vi bare en in-place DB), ID begraensninger m. edge cases osv.)
    //          - Evt metoder, som vi knytter til Customer klassen.
    // testes...

    public class GuestTestingSuite //Should be renamed to CustomerTestingSuite but im scared it deletes the entire document if i do it again:(
    {
        /// <summary>
        /// This tests Customer with object creation and unique ID generation
        /// </summary>
        [Fact]
        public void Customer_Id_ShouldBeSetCorrectly()
        {
            // Arrange
            // var customer = new Customer { Id = 1 };

            // Act
            // int result = customer.Id;

            // Assert
            // Assert.Equal(1, result);
        }

        /// <summary>
        /// This is a summary of Customer_ShouldInheritNameAndEmailFromUser()
        /// 
        /// </summary>
        [Fact]
        public void Customer_ShouldInheritNameAndEmailFromUser()
        {
            // Arrange
            // var customer = new Customer {
            // firstName = "Lars",
            // lastName = "Larsen",
            // address = "Fiskegade 1, 9000 Aalborg",
            // phoneNum = 12345678,
            // email = "larslarsen@live.dk",
            // password = "jegElskerFisk" }

            // Act
            // string firstNameResult = customer.FirstName;
            // string lastNameResult = customer.lastName;
            // string addressResult = customer.Address;
            // string phoneNumbResult = customer.PhoneNum;
            // string emailResult = customer.EmailResult;
            // string passwordResult = customer.PasswordResult;

            // Assert
            // Assert.Equal("Lars", firstNameResult);
            // Assert.Equal("Larsen", lastNameResult);
            // Assert.Equal("Fiskegade 1, 9000 Aalborg", addressResult);
            // Assert.Equal(12345678, phoneNumResult);
            // Assert.Equal("larslarsen@live.dk", emailResult);
            // Assert.Equal("jegElskerFisk", passwordResult);
        }

        /// <summary>
        /// This is a summary of Test_Customer_AppendIdToCustomer()
        /// 
        /// </summary>
        [Fact]
        public void Test_Customer_AppendIdToCustomer()
        {
            // Arrange
            // 

            // Act
            // 

            // Assert
            // 
        }
    }
}