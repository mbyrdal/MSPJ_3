using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MSPJ.TestingEnvironment.ModelSuites
{
    public class ModelsCarTesting
    {
        /// Summary
        /// In this test we test if a car is initialised correctly with default values
        [Fact]
        public void Car_DefaultConstructor_InitialiseDefaultValues()
        {
            // Act
            var car = new Car();

            // Assert
            Assert.Equal(0, car.CarTemplateID);
            Assert.Equal(0, car.ID);
            Assert.Equal(0, car.Mileage);
            Assert.Equal(default(DateTime), car.ProductionYear);
            Assert.Equal(string.Empty, car.VINNumber);
        }

    }
}
