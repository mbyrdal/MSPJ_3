namespace MSPJ_3.TestingEnvironment
{   /// Alt skal ændres inde i denne fil, det er kun kopieret indtil videre
    public class GuestTestingSuite
    {
        /// <summary>
        /// This method tests the default constructor implementation of the Guest class.<br/>
        /// Default/initial values for properties are compared to constructor-assigned ones. <br/>
        /// Finally, the expected vs actual object types are also compared (expecting a Person class object).
        /// </summary>
        [Fact]
        public void Test_Product_CreateObjectUsingDefaultConstructor()
        {
            // Arrange
            // No input arrangements needed since we are testing the default constructor.
            // However, if we have a default value when using the constructor, then we can prepare those...

            // string defaultName = "DefaultName";
            // int defaultPrice = 1000;

            // Act
            // var myProduct = new Product();

            // Assert
            // Assert.IsType<Product>(myProduct); // Expected type is Product
            // Assert.Equal(defaultName, myProduct.Name); // Expected value is "DefaultName"
            // Assert.Equal(defaultPrice, myProduct.Price); // Expected value is 1000
        }

        /// <summary>
        /// This method tests a custom constructor implementation of the Product class.<br/>
        /// Arranged values are compared to constructor-assigned ones.<br/>
        /// Finally, the expected vs actual object types are also compared (expecting a Person class object).
        /// </summary>
        [Fact]
        public void Test_Product_CreateObjectUsingConstructor()
        {
            // Arrange
            // string Name = "Name";
            // int Price = 2500;
            // var DateListed = new DateTime(2020, 10, 11); // October 11th, 2024

            // Act
            // var myProduct = new Product(Name, Price, DateListed);

            // Assert
            // Assert.IsType<Product>(myProduct); // Expected type for myProduct is Product
            // Assert.Equal(Name, myProduct.Name); // Expected value is "DefaultName"
            // Assert.Equal(Price, myProduct.Price); // Expected value is 1000
            // Assert.Equal(DateListed, myProduct.Date); // Expected value is "11/10/2020"
        }
    }
}