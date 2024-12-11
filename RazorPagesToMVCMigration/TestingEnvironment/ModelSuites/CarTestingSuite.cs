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
        //[Fact]
        //public void Car_ShouldInitializeWithCorrectProperties()
        //{
        //    // Arrange & Act
        //    // var car = new CarModel
        //    //  { 
        //    // vinNumber = "ABCDE12345F123456",
        //    // manufacturer = "Honda",
        //    // model = "Civic",
        //    // productionYear = 1997,
        //    // mileage= 123500
        //    //  };

        //    // Assert
        //    // Assert.Equal("ABCDE12345F123456", car.vinNumber)
        //    // Assert.Equal("Honda", car.nimNumber, car.
        //    // Assert.Equal("Civic", car.model)
        //    // Assert.Equal(1997, car.productionYear)
        //    // Assert.Equal(123500, car.mileage)
        //}

        ///// <summary>
        ///// We want the VIN number to follow a specific format,
        ///// 17 char for standard VIN numbers.
        ///// </summary>
        //[Fact]
        //public void Car_VinNumber_ShouldBe17Characters()
        //{
        //    // Arrange
        //    // var car = new CarModel { vinNumber = "1HGCM82633A123456" };

        //    // Act
        //    // int vinLength = car.vinNumber.Length;

        //    // Assert
        //    // Assert.Equal(17, vinLength);
        //}

        ///// <summary>
        ///// We check if the productionYear is valid, meaning any year between now and 1886.
        ///// 1886 is the year the first car was made.
        ///// </summary>
        //[Fact]
        //public void Car_ProductionYearShouldBeValid()
        //{
        //    // Arrange
        //    // var car = new CarModel { productionYear = 2022 };

        //    // Act & Assert
        //    // Assert.InRange(car.productionYear, 1886, DateTime.Now.Year);
        //}

        //[Fact]
        //public void Car_MilageHasToBePositive()
        //{
        //    // Arrange
        //    // var car = new CarModel { mileage = 10000 };

        //    // Act & Assert
        //    // Assert.True(car.mileage >= 0, "Mileage should be positives.");
        //}

        /// <summary>
        /// Ensures that GetCars returns a list of cars when cars exist.
        /// </summary>
        [Fact]
        public void GetCars_ReturnsAllCars_WhenCarsExist()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var cars = new List<Car>
        {
            new Car { ID = 1, VINNumber = "VIN123" },
            new Car { ID = 2, VINNumber = "VIN456" }
        };
            mockCarControl.Setup(ctrl => ctrl.GetAllCars()).Returns(cars);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.GetCars();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCars = Assert.IsAssignableFrom<List<Car>>(okResult.Value);
            Assert.Equal(2, returnedCars.Count);
        }

        /// <summary>
        /// Ensures that GetCars returns NotFound when no cars exist.
        /// </summary>
        [Fact]
        public void GetCars_ReturnsNotFound_WhenNoCarsExist()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            mockCarControl.Setup(ctrl => ctrl.GetAllCars()).Returns(new List<Car>());
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.GetCars();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        /// <summary>
        /// Ensures that GetCarByID returns the correct car when it exists.
        /// </summary>
        [Fact]
        public void GetCarByID_ReturnsCar_WhenCarExists()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var car = new Car { ID = 1, VINNumber = "VIN123" };
            mockCarControl.Setup(ctrl => ctrl.GetCarByID(1)).Returns(car);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.GetCar(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCar = Assert.IsType<Car>(okResult.Value);
            Assert.Equal(1, returnedCar.ID);
            Assert.Equal("VIN123", returnedCar.VINNumber);
        }

        /// <summary>
        /// Ensures that GetCarByID returns NotFound when the car does not exist.
        /// </summary>
        [Fact]
        public void GetCarByID_ReturnsNotFound_WhenCarDoesNotExist()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            mockCarControl.Setup(ctrl => ctrl.GetCarByID(1)).Returns((Car)null);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.GetCar(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        /// <summary>
        /// Ensures that CreateCar returns Created when a car is successfully added.
        /// </summary>
        [Fact]
        public void CreateCar_ReturnsCreated_WhenCarIsAdded()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var car = new Car { ID = 1, VINNumber = "VIN123" };
            mockCarControl.Setup(ctrl => ctrl.AddCar(car)).Returns(true);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.CreateCar(car);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedCar = Assert.IsType<Car>(createdResult.Value);
            Assert.Equal(1, returnedCar.ID);
            Assert.Equal("VIN123", returnedCar.VINNumber);
        }

        /// <summary>
        /// Ensures that CreateCar returns Conflict when the car already exists.
        /// </summary>
        [Fact]
        public void CreateCar_ReturnsConflict_WhenCarAlreadyExists()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var car = new Car { ID = 1, VINNumber = "VIN123" };
            mockCarControl.Setup(ctrl => ctrl.AddCar(car)).Returns(false);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.CreateCar(car);

            // Assert
            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        /// <summary>
        /// Ensures that UpdateCar returns NoContent when a car is successfully updated.
        /// </summary>
        [Fact]
        public void UpdateCar_ReturnsNoContent_WhenCarIsUpdated()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var car = new Car { ID = 1, VINNumber = "VIN123" };
            mockCarControl.Setup(ctrl => ctrl.GetCarByID(1)).Returns(car);
            mockCarControl.Setup(ctrl => ctrl.UpdateCar(car)).Returns(true);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.UpdateCar(1, car);

            // Assert
            Assert.IsType<NoContentResult>(result); // Maybe we should return an "okay" in update car instead of 'NoContent'
        }

        /// <summary>
        /// Ensures that DeleteCar returns NoContent when a car is successfully deleted.
        /// </summary>
        [Fact]
        public void DeleteCar_ReturnsNoContent_WhenCarIsDeleted()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            var car = new Car { ID = 1, VINNumber = "VIN123" };
            mockCarControl.Setup(ctrl => ctrl.GetCarByID(1)).Returns(car);
            mockCarControl.Setup(ctrl => ctrl.DeleteCar(1)).Returns(true);
            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.DeleteCar(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Ensures that DeleteCar returns NotFound when the car does not exist.
        /// This test is here because success returns no content is important to
        /// see that there is in fact an error when you cannot delete the car.
        /// </summary>
        [Fact]
        public void DeleteCar_ReturnsNotFound_WhenCarDoesNotExist()
        {
            // Arrange
            var mockCarControl = new Mock<ICarControl>();
            mockCarControl.Setup(ctrl => ctrl.GetCarByID(1)).Returns((Car?)null);

            var controller = new CarsController(mockCarControl.Object);

            // Act
            var result = controller.DeleteCar(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

    }
}