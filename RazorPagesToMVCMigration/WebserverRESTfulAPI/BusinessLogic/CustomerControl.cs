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
    public class CustomerControl : ICustomerControl
    {
        private readonly DbCustomer _dbCustomerAccess;
        private readonly ILogger<CustomerControl> _logger;

        public CustomerControl(DbCustomer dbCustomerAccess, ILogger<CustomerControl> logger)
        {
            _dbCustomerAccess = dbCustomerAccess;
            _logger = logger;
        }

        public async Task<Customer> GetCustomerByIDAsync(int ID)
        {
            try
            {
                return await _dbCustomerAccess.GetByIdentifierAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving customer with ID: {ID}.");
                return null;
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _dbCustomerAccess.GetAllEntitiesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customers.");
                return null;
            }
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            try
            {
                if (await CustomerExistsAsync(customer.ID, customer.Email))
                    throw new InvalidOperationException($"Customer with ID {customer.ID} and Email {customer.Email} already exists.");

                return await _dbCustomerAccess.CreateEntityAsync(customer) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a customer.");
                return false;
            }
        }

        public async Task<bool> AddCustomerDTOAsync(CustomerViewModel customerDTO)
        {
            try
            {
                if (await CustomerExistsAsync(customerDTO.ID, customerDTO.Email))
                    throw new InvalidOperationException($"Customer with ID {customerDTO.ID} and Email {customerDTO.Email} already exists.");

                return await _dbCustomerAccess.CreateEntityDTOAsync(customerDTO) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a customer (DTO).");
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                if (!await CustomerExistsAsync(customer.ID, customer.Email))
                    throw new InvalidOperationException($"Customer with ID {customer.ID} and Email {customer.Email} does not exist.");

                return await _dbCustomerAccess.UpdateEntityAsync(customer) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a customer.");
                return false;
            }
        }

        public async Task<bool> UpdateCustomerDTOAsync(CustomerViewModel customerDTO)
        {
            try
            {
                if (!await CustomerExistsAsync(customerDTO.ID, customerDTO.Email))
                    throw new InvalidOperationException($"Customer with ID {customerDTO.ID} and Email {customerDTO.Email} does not exist.");

                return await _dbCustomerAccess.UpdateEntityDTOAsync(customerDTO) == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating a customer (DTO).");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int ID)
        {
            try
            {
                var customer = await GetCustomerByIDAsync(ID);
                if (customer == null)
                    throw new InvalidOperationException($"Customer with ID {ID} does not exist.");

                return await _dbCustomerAccess.DeleteEntityAsync(ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting a customer.");
                return false;
            }
        }

        // Private helper to check if a customer exists
        private async Task<bool> CustomerExistsAsync(int ID, string Email)
        {
            try
            {
                return await _dbCustomerAccess.CustomerExistsAsync(ID, Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if a customer exists.");
                throw;
            }
        }
    }
}
