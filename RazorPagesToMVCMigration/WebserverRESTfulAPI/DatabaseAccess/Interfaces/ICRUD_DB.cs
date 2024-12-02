using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.DatabaseAccess.Interfaces
{
    /// <summary>
    /// Interface defining CRUD operations for database access.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface ICRUD_DB<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities asynchronously.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        Task<List<T>> GetAllEntitiesAsync();

        /// <summary>
        /// Retrieves an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>The entity with the given identifier.</returns>
        Task<T> GetByIdentifierAsync(int id);

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> CreateEntityAsync(T entity);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> UpdateEntityAsync(T entity);

        /// <summary>
        /// Deletes an entity by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteEntityAsync(int id);
    }
}
