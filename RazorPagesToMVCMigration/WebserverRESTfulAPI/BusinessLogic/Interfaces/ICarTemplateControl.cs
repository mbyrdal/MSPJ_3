using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface defining business logic operations for managing car templates.
    /// </summary>
    public interface ICarTemplateControl
    {
        /// <summary>
        /// Retrieves a car template by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The car template's ID.</param>
        /// <returns>The car template with the specified ID.</returns>
        Task<CarTemplate> GetCarTemplateByIDAsync(int ID);

        /// <summary>
        /// Retrieves all car templates asynchronously.
        /// </summary>
        /// <returns>A list of all car templates.</returns>
        Task<List<CarTemplate>> GetAllCarTemplatesAsync();

        /// <summary>
        /// Adds a new car template asynchronously.
        /// </summary>
        /// <param name="model">The car template to add.</param>
        /// <returns>A boolean indicating whether the addition was successful.</returns>
        Task<bool> AddCarTemplateAsync(CarTemplate model);

        /// <summary>
        /// Updates an existing car template asynchronously.
        /// </summary>
        /// <param name="model">The car template to update.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateCarTemplateAsync(CarTemplate model);

        /// <summary>
        /// Deletes a car template by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The car template's ID.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteCarTemplateAsync(int ID);

        /// <summary>
        /// Retrieves car templates by their brand asynchronously.
        /// </summary>
        /// <param name="brand">The brand of the car templates to retrieve.</param>
        /// <returns>A list of car templates matching the specified brand.</returns>
        Task<List<CarTemplate>> GetCarTemplatesByBrandAsync(string brand);

        /// <summary>
        /// Retrieves car templates by their model name asynchronously.
        /// </summary>
        /// <param name="modelName">The model name of the car templates to retrieve.</param>
        /// <returns>A list of car templates matching the specified model name.</returns>
        Task<List<CarTemplate>> GetCarTemplatesByModelAsync(string modelName);
    }
}
