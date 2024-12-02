using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface defining asynchronous operations for managing customers.
    /// </summary>
    public interface ICustomerControl
    {
        // CRUD Operations

        /// <summary>
        /// Retrieves a customer by their ID asynchronously.
        /// </summary>
        /// <param name="ID">The customer's ID.</param>
        /// <returns>The customer with the specified ID.</returns>
        Task<Customer> GetCustomerByIDAsync(int ID);

        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        Task<List<Customer>> GetAllCustomersAsync();

        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        /// <returns>A boolean indicating whether the addition was successful.</returns>
        Task<bool> AddCustomerAsync(Customer customer);

        /// <summary>
        /// Adds a new customer using a DTO asynchronously.
        /// </summary>
        /// <param name="customerDTO">The customer DTO to add.</param>
        /// <returns>A boolean indicating whether the addition was successful.</returns>
        Task<bool> AddCustomerDTOAsync(CustomerViewModel customerDTO);

        /// <summary>
        /// Updates an existing customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer to update.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateCustomerAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer using a DTO asynchronously.
        /// </summary>
        /// <param name="customerDTO">The customer DTO to update.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateCustomerDTOAsync(CustomerViewModel customerDTO);

        /// <summary>
        /// Deletes a customer by their ID asynchronously.
        /// </summary>
        /// <param name="ID">The customer's ID.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteCustomerAsync(int ID);

        // Advanced Queries

        /// <summary>
        /// Retrieves a customer by their email address asynchronously.
        /// </summary>
        /// <param name="email">The customer's email address.</param>
        /// <returns>The customer with the specified email address.</returns>
        Task<Customer> GetCustomerByEmailAsync(string email);

        /// <summary>
        /// Retrieves customers by their last name asynchronously.
        /// </summary>
        /// <param name="lastName">The last name to filter by.</param>
        /// <returns>A list of customers matching the specified last name.</returns>
        Task<List<Customer>> GetCustomersByLastNameAsync(string lastName);
    }
}
