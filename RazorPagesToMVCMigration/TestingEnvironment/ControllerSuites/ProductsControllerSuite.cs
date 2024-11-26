using Microsoft.Extensions.Configuration;
using ServiceAPI.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Models;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Utilities;
using ServiceAPI.BusinessLogic;

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
            Assert.Equal("123456", responseList.First().OEM); 
            //Assert.Equal("4Y1-SL658-4-8-Z-41-1439  ", responseList.First().VINNumber);
        }
    }
}
