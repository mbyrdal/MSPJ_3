using Microsoft.Extensions.Configuration;
using ServiceAPI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Models;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Utilities;
using ServiceAPI.BusinessLogic;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;

namespace MSPJ.TestingEnvironment.ControllerSuites
{
    public class ProductsControllerSuite
    {
        /// <summary>
        /// This test checks the CRUD method GetAllProducts() (GET REQUEST) provided by the ProductsController, which relies on the CF Provider ProductControl. <br/>
        /// ProductControl applies business logic to the pipeline. ProductControl uses a DbProduct object from the DAL to handle database access. <br/>
        /// We solve the dependency problem of both ProductControl and ProductsController using Mocks (using Moq package).
        /// </summary>
        [Fact]
        public void GetProducts_GETRequest_ReturnsOkWithListOfProducts()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")["DefaultConnection"])
                             .Returns("Server=hildur.ucn.dk;Database=DMA-CSD-S235_10503098;User ID=DMA-CSD-S235_10503098;Password=Password1!;TrustServerCertificate=true");
            var dbHelper = new ConnectionHelper(mockConfiguration.Object);
            var dbEntityProduct = new DbProduct(mockConfiguration.Object);
            var productControl = new ProductControl(dbEntityProduct);
            // var mockProductControl = new Mock<ProductControl>(mockConfiguration.Object);
            var productController = new ProductsController(productControl);

            // Act
            var response = productController.GetProducts();

            // Assert
            Assert.NotNull(response);
            var actionResponse = Assert.IsType<ActionResult<List<Product>>>(response); // GetProducts return type --> ActionResult<List<Product>>
            var statusResponse = Assert.IsType<OkObjectResult>(response.Result); // Expected return type upon success: Ok(200) / OkObjectResult?
            var responseList = Assert.IsAssignableFrom<List<Product>>(statusResponse.Value); // List of DB products; should be of type List<Product>
            // Assert.Single(responseList); // Only 1 element in the DB (dbo.Product) exists ADJUST TO # OF PRODUCTS IN LIST !!!
            Assert.Equal("WJn-7582", responseList.First().OEM); 
            //Assert.Equal("4Y1-SL658-4-8-Z-41-1439  ", responseList.First().VINNumber);
        }
        // All the tests beneath this comment were created with the help of ChatGPT, and further adapted and verified
        // Here is a link to the chat https://chatgpt.com/share/675ff953-2908-8004-a69e-bba7a6b98069

        /// <summary>
        /// Ensures the GetProducts endpoint returns NotFound when no products exist in the database.
        /// </summary>
        [Fact]
        public void ProductsController_GetProducts_ReturnsNotFound_WhenNoProductsExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.GetAllProducts()).Returns(new List<Product>());
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("ERROR: No products found in the database.", actionResult.Value);
        }

        /// <summary>
        /// Ensures the GetProducts endpoint returns a list of products when products exist in the database.
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
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsType<List<Product>>(actionResult.Value);
            Assert.Equal(2, returnedProducts.Count);
        }

        /// <summary>
        /// Ensures the GetProductUsingOEM endpoint returns NotFound when the requested product does not exist.
        /// </summary>
        [Fact]
        public void ProductsController_GetProductUsingOEM_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.GetProductID(It.IsAny<string>())).Returns(0);
            mockProductControl.Setup(pc => pc.GetProductByID(It.IsAny<int>())).Returns((Product)null);
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.GetProductUsingOEM("InvalidOEM");

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Product with OEM 'InvalidOEM' not found.", actionResult.Value);
        }

        /// <summary>
        /// Ensures the CreateProduct endpoint returns BadRequest when the input product is null.
        /// </summary>
        [Fact]
        public void ProductsController_CreateProduct_ReturnsBadRequest_WhenProductIsNull()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.CreateProduct("PartName", "VIN123", null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        /// <summary>
        /// Ensures the CreateProduct endpoint returns Conflict when the product already exists in the database.
        /// </summary>
        [Fact]
        public void ProductsController_CreateProduct_ReturnsConflict_WhenProductAlreadyExists()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.AddProduct(It.IsAny<ProductDTO>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductDTO { ID = 1, OEM = "OEM123" };

            // Act
            var result = controller.CreateProduct("PartName", "VIN123", productViewModel);

            // Assert
            var actionResult = Assert.IsType<ConflictObjectResult>(result.Result);
        }

        /// <summary>
        /// Ensures the UpdateProduct endpoint returns NoContent when the product is successfully updated.
        /// </summary>
        [Fact]
        public void ProductsController_UpdateProduct_ReturnsNoContent_WhenUpdateSucceeds()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            var existingProduct = new Product { OEM = "OEM123" };
            mockProductControl.Setup(pc => pc.GetProduct(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(existingProduct);
            mockProductControl.Setup(pc => pc.UpdateProduct(It.IsAny<string>(), It.IsAny<ProductDTO>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductDTO { OEM = "OEM123" };

            // Act
            var result = controller.UpdateProduct("OEM123", "PartName", "VIN123", productViewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Ensures the DeleteProduct endpoint returns NotFound when the requested product does not exist.
        /// </summary>
        [Fact]
        public void ProductsController_DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.GetProductByID(It.IsAny<int>())).Returns((Product)null);
            var controller = new ProductsController(mockProductControl.Object);

            // Act
            var result = controller.DeleteProduct("InvalidOEM");

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No existing Product with OEM '' found.", actionResult.Value);
        }
    }
}
