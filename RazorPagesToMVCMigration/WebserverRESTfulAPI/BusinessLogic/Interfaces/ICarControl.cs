using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface defining business logic operations for managing cars.
    /// </summary>
    public interface ICarControl
    {
        /// <summary>
        /// Retrieves a car by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The car's ID.</param>
        /// <returns>The car with the specified ID.</returns>
        Task<Car> GetCarByIDAsync(int ID);

        /// <summary>
        /// Retrieves all cars asynchronously.
        /// </summary>
        /// <returns>A list of all cars.</returns>
        Task<List<Car>> GetAllCarsAsync();

        /// <summary>
        /// Adds a new car asynchronously.
        /// </summary>
        /// <param name="model">The 
