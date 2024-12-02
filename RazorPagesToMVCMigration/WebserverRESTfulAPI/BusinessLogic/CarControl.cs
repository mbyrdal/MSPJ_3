using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic
{
    public class CarControl : ICarControl
    {
        private readonly DbCar _dbCarAccess;
        private readonly ILogger<CarControl> _logger;

        public CarControl(DbCar dbCarAccess, ILogger<CarControl> logger)
        {
            _dbCarAccess = dbCarAccess;
            _logger = logger;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            try
            {
                return await _dbCarAccess.GetAllEntitiesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all cars.");
                return null;
            }
        }

        public async Task<Car> GetCarByIDAsync(int ID)
        {
            try
            {
                return await _dbCarAccess.GetByIdentifierAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving car with ID: {ID}.");
                return null;
            }
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            try
            {
                if (await CarExistsAsync(car.ID, car.VINNumber))
                    throw new InvalidOperationException($"Car with ID {car.ID} and VIN {car.VINNumber} already exists.");

                return await _dbCarAccess.CreateEntityAsync(car) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a car.");
                return false;
            }
        }

        public async Task<bool> AddCarDTOAsync(CarViewModel car)
        {
            try
            {
                if (await CarExistsAsync(car.ID, car.VINNumber))
                    throw new InvalidOperationException($"Car with ID {car.ID} and VIN {car.VINNumber} already exists.");

                return await _dbCarAccess.CreateEntityDTOAsync(car) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a car (DTO).");
                return false;
            }
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            try
            {
                if (!await CarExistsAsync(car.ID, car.VINNumber))
                    throw new InvalidOperationException($"Car with ID {car.ID} and VIN {car.VINNumber} does not exist.");

                return await _dbCarAccess.UpdateEntityAsync(car) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a car.");
                return false;
            }
        }

        public async Task<bool> UpdateCarDTOAsync(CarViewModel car)
        {
            try
            {
                if (!await CarExistsAsync(car.ID, car.VINNumber))
                    throw new InvalidOperationException($"Car with ID {car.ID} and VIN {car.VINNumber} does not exist.");

                return await _dbCarAccess.UpdateEntityDTOAsync(car) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a car (DTO).");
                return false;
            }
        }

        public async Task<bool> DeleteCarAsync(int ID)
        {
            try
            {
                var car = await GetCarByIDAsync(ID);
                if (car == null)
                    throw new InvalidOperationException($"Car with ID {ID} does not exist.");

                return await _dbCarAccess.DeleteEntityAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a car.");
                return false;
            }
        }

        // Private helper to check if a car exists
        private async Task<bool> CarExistsAsync(int ID, string VINNumber)
        {
            try
            {
                return await _dbCarAccess.CarExistsAsync(ID, VINNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if a car exists.");
                throw;
            }
        }
    }
}
