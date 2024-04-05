namespace Core.DataAccess.IRepository
{
    /// <summary>
    /// Generic Repository Interface
    /// </summary>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Saves an entity
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <param name="cancellationToken"></param>
        Task SaveAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Saves a collection of entities. 
        /// </summary>
        /// <param name="entities">The entities to be saved</param>
        /// <param name="cancellationToken"></param>
        Task SaveAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be updated</param>
        /// <param name="cancellationToken"></param>
        Task UpdateAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an Entity
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be deleted</param>
        /// <param name="cancellationToken"></param>
        Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity by it's Id
        /// </summary>
        /// <param name="id">The Id of the entity to be deleted</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="OperationCanceledException"></exception>
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets an Entity by it's Id
        /// </summary>
        /// <param name="id">The id of the entity to be found</param>
        /// <param name="cancellationToken"></param>
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Entities by a list of Id's 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}
