using Moq;
using Xunit;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Controllers;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MSPJ.TestingEnvironment.ModelSuites
{
    public class ProductsControllerTests
    {
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
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
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
            var actionResult = Assert.IsType<OkObjectResult>(result);
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
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
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
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Ensures the CreateProduct endpoint returns Conflict when the product already exists in the database.
        /// </summary>
        [Fact]
        public void ProductsController_CreateProduct_ReturnsConflict_WhenProductAlreadyExists()
        {
            // Arrange
            var mockProductControl = new Mock<IProductControl>();
            mockProductControl.Setup(pc => pc.AddProduct(It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { ID = 1, OEM = "OEM123" };

            // Act
            var result = controller.CreateProduct("PartName", "VIN123", productViewModel);

            // Assert
            var actionResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal($"ERROR: Product with OEM '{productViewModel.OEM}' already exists in the database, or insertion failed in another manner.", actionResult.Value);
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
            mockProductControl.Setup(pc => pc.UpdateProduct(It.IsAny<string>(), It.IsAny<ProductViewModel>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var controller = new ProductsController(mockProductControl.Object);
            var productViewModel = new ProductViewModel { OEM = "OEM123" };

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
