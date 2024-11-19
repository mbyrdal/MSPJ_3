using Microsoft.Extensions.Configuration;
using ServiceAPI.BusinessLogic.Services;
using ServiceAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.Models;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DatabaseAccess.DatabaseEntities;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Hosting.Server;

namespace MSPJ.TestingEnvironment.ControllerSuites
{
    public class ProductsControllerSuite
    {
        /// <summary>
        /// This test checks the CRUD functionality (GET REQUEST) provided by the ProductsController, which relies on the Service Provider ProductService. <br/>
        /// ProductService applies business logic to the pipeline. ProductService uses a DbEntity_Product object from the DAL to handle database access. <br/>
        /// We solve the dependency problem of both Service and Controller using Mocks (using Moq package).
        /// </summary>
        [Fact]
        public void GetProducts_GETRequest_ReturnsOkWithListOfProducts()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")["DefaultConnection"])
                             .Returns("Server=hildur.ucn.dk;Database=DMA-CSD-S235_10503098;User Id=DMA-CSD-S235_10503098;Password=Password1!;TrustServerCertificate=true");
            var dbHelper = new DbHelper(mockConfiguration.Object);
            var dbEntityProduct = new DbEntity_Product(mockConfiguration.Object);
            var mockProductService = new Mock<ProductService>(mockConfiguration.Object);
            var productController = new ProductsController(mockProductService.Object);

            // Act
            var response = productController.GetProducts();

            // Assert
            Assert.NotNull(response);
            var actionResponse = Assert.IsType<ActionResult<IEnumerable<Product>>>(response); // GetProducts return type --> ActionResult<IEnumerable<Product>>
            var statusResponse = Assert.IsType<OkObjectResult>(response.Result); // Expected return type upon success: Ok(200) / OkObjectResult?
            var responseList = Assert.IsAssignableFrom<IEnumerable<Product>>(statusResponse.Value); // List of DB products; should be of type IEnumerable<Product>
            Assert.Single(responseList); // Only 1 element in the DB (dbo.Product) exists
            Assert.Equal("123456", responseList.First().OEM); 
            Assert.Equal("4Y1-SL658-4-8-Z-41-1439  ", responseList.First().VINNumber);
        }
    }
}
