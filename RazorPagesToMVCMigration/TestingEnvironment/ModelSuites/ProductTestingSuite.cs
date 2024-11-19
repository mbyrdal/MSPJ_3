using ServiceAPI.Models;

namespace MSPJ.TestingEnvironment.ModelSuites
{
    public class ProductTestingSuite
    {
        /// <summary>
        /// This method tests the default constructor implementation of the Product class.<br/>
        /// Default/initial values for properties are compared to constructor-assigned ones. <br/>
        /// Finally, the expected vs actual object types are also compared (expecting a Product class object).
        /// </summary>
        [Fact]
        public void Product_DefaultConstructor_CreatesUserUsingDefaultConstructor()
        {
            // Arrange
            string defaultOEM = string.Empty;
            string defaultVINNumber = string.Empty;
            string defaultName = "ProductName";
            decimal defaultPrice = 0;
            DateTime defaultDateAvailable = DateTime.UnixEpoch;
            string defaultNotes = string.Empty;

            // Act
            var myProduct = new Product();

            // Assert
            Assert.IsType<Product>(myProduct); // Expected type is Product
            Assert.Equal(defaultOEM, myProduct.OEM); // Expected value is string.Empty ("")
            Assert.Equal(defaultVINNumber, myProduct.VINNumber); // Expected value is string.Empty ("")
            Assert.Equal(defaultName, myProduct.Name); // Expected value is "ProductName" 
            Assert.Equal(defaultPrice, myProduct.Price); // Expected value is 0
            Assert.Equal(defaultDateAvailable, myProduct.DateAvailable); // Expected value is UnixEpoch (Jan 1, 1970)
            Assert.Equal(defaultNotes, myProduct.Notes); // Expected value is ("") 
        }

        /// <summary>
        /// This method tests a custom constructor implementation of the Product class.<br/>
        /// Arranged values are compared to constructor-assigned ones.<br/>
        /// Finally, the expected vs actual object types are also compared (expecting a Product class object).
        /// </summary>
        [Fact]
        public void Product_CustomConstructor_CreatesUserUsingCustomConstructor()
        {
            // Arrange
            string oem = "17220-R70-A00"; // Engine Air Filter
            string vinnumber = "1HGCM82633A123456"; // ChatGPT 2003 Honda Accord EX V6 Sedan
            string name = "Engine Air Filter";
            decimal price = 750;
            DateTime dateAvailable = new DateTime(2024, 5, 30); // May 30th, 2024
            string notes = "None";

            // Act
            var myProduct = new Product(oem, vinnumber, name, price, dateAvailable, notes);

            // Assert
            Assert.IsType<Product>(myProduct); // Expected type for myProduct is Product
            Assert.Equal(oem, myProduct.OEM); // Expected value is "17220-R70-A00"
            Assert.Equal(vinnumber, myProduct.VINNumber); // Expected value is "1HGCM82633A123456"
            Assert.Equal(name, myProduct.Name); // Expected value is "Engine Air Filter"
            Assert.Equal(price, myProduct.Price); // Expected value is 750
            Assert.Equal(dateAvailable, myProduct.DateAvailable); // Expected value is "30/5/2024"
            Assert.Equal(notes, myProduct.Notes); // Expected value is "None"
        }
    }
}