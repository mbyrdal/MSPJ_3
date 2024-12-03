using Moq;
using Xunit;
using ServiceAPI.BusinessLogic;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Controllers;

namespace MSPJ.TestingEnvironment.ModelSuites
{   /// <summary>
    /// 
    /// </summary>

    public class CarTestingSuite //Should be renamed to CustomerTestingSuite but im scared it deletes the entire document if i do it again:(
    {
        /// <summary>
        /// NEEDED
        /// </summary>
        [Fact]
        public void Car_ShouldInitializeWithCorrectProperties()
        {
            // Arrange & Act
            // var car = new CarModel
            //  { 
            // vinNumber = "ABCDE12345F123456",
            // manufacturer = "Honda",
            // model = "Civic",
            // productionYear = 1997,
            // mileage= 123500
            //  };

            // Assert
            // Assert.Equal("ABCDE12345F123456", car.vinNumber)
            // Assert.Equal("Honda", car.nimNumber, car.
            // Assert.Equal("Civic", car.model)
            // Assert.Equal(1997, car.productionYear)
            // Assert.Equal(123500, car.mileage)
        }

        /// <summary>
        /// We want the VIN number to follow a specific format,
        /// 17 char for standard VIN numbers.
        /// </summary>
        [Fact]
        public void Car_VinNumber_ShouldBe17Characters()
        {
            // Arrange
            // var car = new CarModel { vinNumber = "1HGCM82633A123456" };

            // Act
            // int vinLength = car.vinNumber.Length;

            // Assert
            // Assert.Equal(17, vinLength);
        }

        /// <summary>
        /// We check if the productionYear is valid, meaning any year between now and 1886.
        /// 1886 is the year the first car was made.
        /// </summary>
        [Fact]
        public void Car_ProductionYearShouldBeValid()
        {
            // Arrange
            // var car = new CarModel { productionYear = 2022 };

            // Act & Assert
            // Assert.InRange(car.productionYear, 1886, DateTime.Now.Year);
        }

        [Fact]
        public void Car_MilageHasToBePositive()
        {
            // Arrange
            // var car = new CarModel { mileage = 10000 };

            // Act & Assert
            // Assert.True(car.mileage >= 0, "Mileage should be positives.");
        }

        /// <summary>
        /// We should get all cars if they exist
        /// </summary>
        public class CarsControllerTests
        {
            // Test for GetCars method: returns all cars when they exist.
            [Fact]
            public async Task GetCars_ReturnsAllCars_WhenCarsExist()
            {
                // Arrange: Mock ICarControl and set up GetAllCarsAsync to return a list of cars.
                var mockCarControl = new Mock<ICarControl>();
                var cars = new List<Car>
        {
            new Car { ID = 1, VINNumber = "VIN123" },
            new Car { ID = 2, VINNumber = "VIN456" }
        };
                mockCarControl.Setup(ctrl => ctrl.GetAllCarsAsync()).ReturnsAsync(cars);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the GetCars method.
                var result = await controller.GetCars();

                // Assert: Ensure the response is OK with the correct number of cars.
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedCars = Assert.IsAssignableFrom<List<Car>>(okResult.Value);
                Assert.Equal(2, returnedCars.Count);
            }

            // Test for GetCars method: returns NotFound when no cars exist.
            [Fact]
            public async Task GetCars_ReturnsNotFound_WhenNoCarsExist()
            {
                // Arrange: Mock ICarControl to return an empty list of cars.
                var mockCarControl = new Mock<ICarControl>();
                mockCarControl.Setup(ctrl => ctrl.GetAllCarsAsync()).ReturnsAsync(new List<Car>());

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the GetCars method.
                var result = await controller.GetCars();

                // Assert: Ensure the response is NotFound when no cars exist.
                Assert.IsType<NotFoundObjectResult>(result.Result);
            }

            // Test for GetCarByID method: returns a car when it exists.
            [Fact]
            public async Task GetCarByID_ReturnsCar_WhenCarExists()
            {
                // Arrange: Mock ICarControl to return a specific car.
                var mockCarControl = new Mock<ICarControl>();
                var car = new Car { ID = 1, VINNumber = "VIN123" };
                mockCarControl.Setup(ctrl => ctrl.GetCarByIDAsync(1)).ReturnsAsync(car);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the GetCarByID method.
                var result = await controller.GetCar(1);

                // Assert: Ensure the response is OK with the correct car.
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedCar = Assert.IsType<Car>(okResult.Value);
                Assert.Equal(1, returnedCar.ID);
                Assert.Equal("VIN123", returnedCar.VINNumber);
            }

            // Test for GetCarByID method: returns NotFound when the car does not exist.
            [Fact]
            public async Task GetCarByID_ReturnsNotFound_WhenCarDoesNotExist()
            {
                // Arrange: Mock ICarControl to return null when searching by ID.
                var mockCarControl = new Mock<ICarControl>();
                mockCarControl.Setup(ctrl => ctrl.GetCarByIDAsync(1)).ReturnsAsync((Car)null);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the GetCarByID method.
                var result = await controller.GetCar(1);

                // Assert: Ensure the response is NotFound when the car doesn't exist.
                Assert.IsType<NotFoundObjectResult>(result.Result);
            }

            // Test for CreateCar method: successfully creates a car.
            [Fact]
            public async Task CreateCar_ReturnsCreated_WhenCarIsAdded()
            {
                // Arrange: Mock ICarControl to return true for car creation.
                var mockCarControl = new Mock<ICarControl>();
                var car = new Car { ID = 1, VINNumber = "VIN123" };
                mockCarControl.Setup(ctrl => ctrl.AddCarAsync(car)).ReturnsAsync(true);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the CreateCar method.
                var result = await controller.CreateCar(car);

                // Assert: Ensure the response is Created with the correct car.
                var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var returnedCar = Assert.IsType<Car>(createdResult.Value);
                Assert.Equal(1, returnedCar.ID);
                Assert.Equal("VIN123", returnedCar.VINNumber);
            }

            // Test for CreateCar method: returns Conflict when car already exists.
            [Fact]
            public async Task CreateCar_ReturnsConflict_WhenCarAlreadyExists()
            {
                // Arrange: Mock ICarControl to return false for car creation (car already exists).
                var mockCarControl = new Mock<ICarControl>();
                var car = new Car { ID = 1, VINNumber = "VIN123" };
                mockCarControl.Setup(ctrl => ctrl.AddCarAsync(car)).ReturnsAsync(false);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the CreateCar method.
                var result = await controller.CreateCar(car);

                // Assert: Ensure the response is Conflict when the car already exists.
                Assert.IsType<ConflictObjectResult>(result.Result);
            }

            // Test for UpdateCar method: successfully updates a car.
            [Fact]
            public async Task UpdateCar_ReturnsNoContent_WhenCarIsUpdated()
            {
                // Arrange: Mock ICarControl to simulate car existence and successful update.
                var mockCarControl = new Mock<ICarControl>();
                var car = new Car { ID = 1, VINNumber = "VIN123" };
                mockCarControl.Setup(ctrl => ctrl.GetCarByIDAsync(1)).ReturnsAsync(car);
                mockCarControl.Setup(ctrl => ctrl.UpdateCarAsync(car)).ReturnsAsync(true);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the UpdateCar method.
                var result = await controller.UpdateCar(1, car);

                // Assert: Ensure the response is NoContent after successful update.
                Assert.IsType<NoContentResult>(result);
            }

            // Test for DeleteCar method: successfully deletes a car.
            [Fact]
            public async Task DeleteCar_ReturnsNoContent_WhenCarIsDeleted()
            {
                // Arrange: Mock ICarControl to simulate car existence and successful deletion.
                var mockCarControl = new Mock<ICarControl>();
                var car = new Car { ID = 1, VINNumber = "VIN123" };
                mockCarControl.Setup(ctrl => ctrl.GetCarByIDAsync(1)).ReturnsAsync(car);
                mockCarControl.Setup(ctrl => ctrl.DeleteCarAsync(1)).ReturnsAsync(true);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the DeleteCar method.
                var result = await controller.DeleteCar(1);

                // Assert: Ensure the response is NoContent after successful deletion.
                Assert.IsType<NoContentResult>(result);
            }

            // Test for DeleteCar method: returns NotFound when car does not exist.
            [Fact]
            public async Task DeleteCar_ReturnsNotFound_WhenCarDoesNotExist()
            {
                // Arrange: Mock ICarControl to return null when trying to delete a non-existing car.
                var mockCarControl = new Mock<ICarControl>();
                mockCarControl.Setup(ctrl => ctrl.GetCarByIDAsync(1)).ReturnsAsync((Car)null);

                var controller = new CarsController(mockCarControl.Object);

                // Act: Call the DeleteCar method.
                var result = await controller.DeleteCar(1);

                // Assert: Ensure the response is NotFound when the car doesn't exist.
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

    }
}