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
        /// Deletes an entity by Id
        /// </summary>
        /// <param name="id">Entity Id to be deleted</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="OperationCanceledException"></exception>
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets an Entity by Id
        /// </summary>
        /// <param name="id">Entity Id to be found</param>
        /// <param name="cancellationToken"></param>
        Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Entities by Id List
        /// </summary>
        /// <param name="ids">Id List</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    }
}
