using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    /// <summary>
    /// Interface defining asynchronous CRUD operations for a generic entity type.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface ICRUD<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The entity's ID.</param>
        /// <returns>The entity with the specified ID.</returns>
        Task<T> GetByIDAsync(int ID);

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A boolean indicating whether the creation was successful.</returns>
        Task<bool> CreateAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A boolean indicating whether the update was successful.</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The entity's ID.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(int ID);

        /// <summary>
        /// Checks if an entity exists by its ID asynchronously.
        /// </summary>
        /// <param name="ID">The entity's ID.</param>
        /// <returns>A boolean indicating whether the entity exists.</returns>
        Task<bool> EntityExistsAsync(int ID);
    }
}
