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
    public class ProductControl : IProductControl
    {
        private readonly DbProduct _dbProductAccess;
        private readonly ILogger<ProductControl> _logger;

        public ProductControl(DbProduct dbProductAccess, ILogger<ProductControl> logger)
        {
            _dbProductAccess = dbProductAccess;
            _logger = logger;
        }

        public async Task<Product> GetProductByIDAsync(int ID)
        {
            try
            {
                return await _dbProductAccess.GetByIdentifierAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving product with ID: {ID}.");
                return null;
            }
        }

        public async Task<Product> GetProductByOEMAsync(string OEM)
        {
            try
            {
                int productID = await _dbProductAccess.GetProductIDByOEMAsync(OEM);
                return await _dbProductAccess.GetByIdentifierAsync(productID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving product with OEM: {OEM}.");
                return null;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _dbProductAccess.GetAllEntitiesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all products.");
                return null;
            }
        }

        public async Task<bool> AddProductAsync(ProductViewModel product, string carPartName, string carVINNumber)
        {
            try
            {
                if (!await _dbProductAccess.CarAndCarPartExistsAsync(carPartName, carVINNumber))
                {
                    throw new InvalidOperationException("Either the car or the car part does not exist.");
                }

                int carPartID = await _dbProductAccess.GetCarPartIDByNameAsync(carPartName);
                int carID = await _dbProductAccess.GetCarByVINNumberAsync(carVINNumber);

                if (await _dbProductAccess.ProductExistsAsync(product.ID))
                {
                    throw new InvalidOperationException($"Product with ID {product.ID} already exists.");
                }

                int rowsInserted = await _dbProductAccess.CreateEntityDTOAsync(product, carPartID, carID);
                return rowsInserted == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a product.");
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(string OEM, ProductViewModel product, string carPartName, string carVINNumber)
        {
            try
            {
                if (!await _dbProductAccess.CarAndCarPartExistsAsync(carPartName, carVINNumber))
                {
                    throw new ArgumentException($"Car with VIN {carVINNumber} or car part {carPartName} does not exist.");
                }

                int carPartID = await _dbProductAccess.GetCarPartIDByNameAsync(carPartName);
                int carID = await _dbProductAccess.GetCarByVINNumberAsync(carVINNumber);
                int existingProductID = await _dbProductAccess.GetProductIDByOEMAsync(OEM);

                if (!await _dbProductAccess.ProductExistsAsync(existingProductID))
                {
                    throw new InvalidOperationException($"Product with OEM {OEM} does not exist.");
                }

                int rowsUpdated = await _dbProductAccess.UpdateEntityAsync(product, existingProductID, carPartID, carID);
                return rowsUpdated == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a product.");
                return false;
            }
        }

        public async Task<bool> DeleteProductAsync(string OEM)
        {
            try
            {
                int productID = await _dbProductAccess.GetProductIDByOEMAsync(OEM);

                if (!await _dbProductAccess.ProductExistsAsync(productID))
                {
                    throw new InvalidOperationException($"Product with OEM {OEM} does not exist.");
                }

                return await _dbProductAccess.DeleteEntityAsync(productID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a product.");
                return false;
            }
        }
    }
}
