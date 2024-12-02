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
    public class CarTemplateControl : ICarTemplateControl
    {
        private readonly DbCarTemplate _dbCarTemplateAccess;
        private readonly ILogger<CarTemplateControl> _logger;

        public CarTemplateControl(DbCarTemplate dbCarTemplateAccess, ILogger<CarTemplateControl> logger)
        {
            _dbCarTemplateAccess = dbCarTemplateAccess;
            _logger = logger;
        }

        public async Task<List<CarTemplate>> GetAllCarTemplatesAsync()
        {
            try
            {
                return await _dbCarTemplateAccess.GetAllEntitiesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all car templates.");
                return null;
            }
        }

        public async Task<CarTemplate> GetCarTemplateByIDAsync(int ID)
        {
            try
            {
                return await _dbCarTemplateAccess.GetByIdentifierAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving car template with ID: {ID}.");
                return null;
            }
        }

        public async Task<bool> AddCarTemplateAsync(CarTemplate carTemplate)
        {
            try
            {
                if (await CarTemplateExistsAsync(carTemplate.ID))
                    throw new InvalidOperationException($"Car template with ID {carTemplate.ID} already exists.");

                return await _dbCarTemplateAccess.CreateEntityAsync(carTemplate) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a car template.");
                return false;
            }
        }

        public async Task<bool> UpdateCarTemplateAsync(CarTemplate carTemplate)
        {
            try
            {
                if (!await CarTemplateExistsAsync(carTemplate.ID))
                    throw new InvalidOperationException($"Car template with ID {carTemplate.ID} does not exist.");

                return await _dbCarTemplateAccess.UpdateEntityAsync(carTemplate) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a car template.");
                return false;
            }
        }

        public async Task<bool> DeleteCarTemplateAsync(int ID)
        {
            try
            {
                if (!await CarTemplateExistsAsync(ID))
                    throw new InvalidOperationException($"Car template with ID {ID} does not exist.");

                return await _dbCarTemplateAccess.DeleteEntityAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a car template.");
                return false;
            }
        }

        // Private helper to check if a car template exists
        private async Task<bool> CarTemplateExistsAsync(int ID)
        {
            try
            {
                return await _dbCarTemplateAccess.EntityExistsAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if a car template exists.");
                throw;
            }
        }
    }
}
