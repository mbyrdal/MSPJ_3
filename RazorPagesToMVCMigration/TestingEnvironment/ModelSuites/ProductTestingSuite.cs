using Moq;
using Xunit;
using ServiceAPI.BusinessLogic;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Controllers;

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

        /// <summary>
        /// Ensures the DeleteProduct endpoint returns a NotFound response when the product does not exist.
        /// </summary>
        [Fact]
        public void ProductsController_DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.GetProductByOEM(It.IsAny<string>())).Returns((Product)null); // Simulate product not found
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.DeleteProduct("OEM123");

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result); // Expected: NotFound response
            Assert.Equal("No existing Product with OEM 'OEM123' found.", actionResult.Value); // Expected message
        }

        /// <summary>
        /// Ensures the DeleteProduct endpoint returns a NoContent response when the product exists and is deleted successfully.
        /// </summary>
        [Fact]
        public void ProductsController_DeleteProduct_ReturnsNoContent_WhenProductExists()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var mockProduct = new Product { OEM = "OEM123" };
            mockProductControl.Setup(pc => pc.GetProductByOEM(It.IsAny<string>())).Returns(mockProduct);
            mockProductControl.Setup(pc => pc.DeleteProduct(It.IsAny<string>())).Returns(true);
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.DeleteProduct("OEM123");

            // Assert
            Assert.IsType<NoContentResult>(result); // Expected: NoContent (204) response
        }

        /// <summary>
        /// Ensures the GetProduct endpoint returns the requested product when it exists.
        /// </summary>
        [Fact]
        public void ProductsController_GetProduct_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var mockProduct = new Product { ID = 1, OEM = "OEM123" };
            mockProductControl.Setup(pc => pc.GetProductByOEM(It.IsAny<string>())).Returns(mockProduct);
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.GetProduct("OEM123");

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result); // Expected: OK response
            var returnedProduct = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(mockProduct.ID, returnedProduct.ID);
            Assert.Equal(mockProduct.OEM, returnedProduct.OEM);
        }

        /// <summary>
        /// Ensures the AddProduct endpoint returns a Conflict response when the related car or car part does not exist.
        /// </summary>
        [Fact]
        public void ProductsController_AddProduct_ReturnsBadRequest_WhenCarOrCarPartDoesNotExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.AddProduct(It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { ID = 1, OEM = "OEM123" };

            // Act
            var result = controller.CreateProduct(productViewModel, "PartName", "VIN123");

            // Assert
            var actionResult = Assert.IsType<ConflictObjectResult>(result); // Expected: Conflict (409)
        }

        /// <summary>
        /// Ensures the AddProduct endpoint returns a Created response when the product is successfully added.
        /// </summary>
        [Fact]
        public void ProductsController_AddProduct_ReturnsCreated_WhenCarAndCarPartExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.AddProduct(It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { ID = 1, OEM = "OEM123" };

            // Act
            var result = controller.CreateProduct(productViewModel, "PartName", "VIN123");

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result); // Expected: Created response
            Assert.Equal(productViewModel, actionResult.Value);
        }

        /// <summary>
        /// Ensures the AddProduct endpoint returns a Conflict response when the product already exists.
        /// </summary>
        [Fact]
        public void ProductsController_AddProduct_ReturnsConflict_WhenProductAlreadyExists()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.AddProduct(It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { ID = 1, OEM = "OEM123" };

            // Act
            var result = controller.CreateProduct(productViewModel, "PartName", "VIN123");

            // Assert
            var actionResult = Assert.IsType<ConflictObjectResult>(result); // Expected: Conflict response
        }

        /// <summary>
        /// Ensures the DeleteProduct endpoint throws an exception when a database error occurs.
        /// </summary>
        [Fact]
        public void ProductsController_DeleteProduct_ThrowsException_WhenDatabaseErrorOccurs()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.GetProductByOEM(It.IsAny<string>())).Throws(new Exception("Database error"));
            var controller = new ProductsController(mockProductControl.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => controller.DeleteProduct("OEM123"));
            Assert.Equal("Database error", exception.Message);
        }

        /// <summary>
        /// Ensures the GetProducts endpoint returns a list of products when they exist.
        /// </summary>
        [Fact]
        public void ProductsController_GetProducts_ReturnsListOfProducts_WhenProductsExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var products = new List<Product>
    {
        new Product { ID = 1, OEM = "OEM123" },
        new Product { ID = 2, OEM = "OEM456" }
    };
            mockProductControl.Setup(pc => pc.GetAllProducts()).Returns(products);
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result); // Expected: OK response
            var returnedProducts = Assert.IsType<List<Product>>(actionResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        /// <summary>
        /// Ensures the UpdateProduct endpoint returns a NoContent response when the update is successful.
        /// </summary>
        [Fact]
        public void ProductsController_UpdateProduct_ReturnsNoContent_WhenUpdateSucceeds()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var mockProduct = new Product { OEM = "OEM123" };
            mockProductControl.Setup(pc => pc.GetProductByOEM(It.IsAny<string>())).Returns(mockProduct);
            mockProductControl.Setup(pc => pc.UpdateProduct(It.IsAny<string>(), It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { OEM = "OEM123" };

            // Act
            var result = controller.UpdateProduct("OEM123", productViewModel, "PartName", "VIN123");

            // Assert
            Assert.IsType<NoContentResult>(result); // Expected: NoContent (204)
        }

        /// <summary>
        /// Verifies that the Product custom constructor handles null and empty values properly.
        /// </summary>
        [Fact]
        public void Product_CustomConstructor_HandlesEmptyStringsAndNullValues()
        {
            // Arrange
            string oem = null;
            string vinnumber = string.Empty;
            string name = null;
            decimal price = 0;
            DateTime dateAvailable = default;
            string notes = string.Empty;

            // Act
            var myProduct = new Product(oem, vinnumber, name, price, dateAvailable, notes);

            // Assert
            Assert.Null(myProduct.OEM);
            Assert.Equal(string.Empty, myProduct.VINNumber);
            Assert.Null(myProduct.Name);
            Assert.Equal(0, myProduct.Price);
            Assert.Equal(default, myProduct.DateAvailable);
            Assert.Equal(string.Empty, myProduct.Notes);
        }

    }
}