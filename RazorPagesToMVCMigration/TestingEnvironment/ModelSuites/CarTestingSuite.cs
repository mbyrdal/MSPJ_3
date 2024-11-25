using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

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
    }
}