using Core.DataAccess.IRepository;
using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repository
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext context;
        protected DbSet<TEntity> dbSet;

        /// <summary>
        /// Initialices a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">Application DbContext</param>
        protected GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Saves an entity
        /// </summary>
        /// <param name="entity">The entity to be saved</param>
        /// <param name="cancellationToken"></param>
        public async Task SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await dbSet.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Saves a collection of entities. 
        /// </summary>
        /// <param name="entities">The entities to be saved</param>
        /// <param name="cancellationToken"></param>
        public async Task SaveAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
                await SaveAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The entity to be updated</param>
        /// <param name="cancellationToken"></param>
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be updated</param>
        /// <param name="cancellationToken"></param>
        public async Task UpdateAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            foreach(var entity in entities)
                await UpdateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Deletes an Entity
        /// </summary>
        /// <param name="entity">The entity to be deleted</param>
        /// <param name="cancellationToken"></param>
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be deleted</param>
        /// <param name="cancellationToken"></param>
        public async Task DeleteAllAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            foreach( var entity in entities)
                await DeleteAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Deletes an entity by Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                throw new OperationCanceledException(id + " is not valid");

            TEntity entity = await GetByIdAsync(id, cancellationToken);

            if (entity != null)
                await DeleteAsync(entity, cancellationToken);
            else
                throw new OperationCanceledException("Entity does not exist");
        }

        /// <summary>
        /// Gets an Entity by Id
        /// </summary>
        /// <param name="id">Entity Id</param>
        /// <param name="cancellationToken"></param>
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if(id <= 0)
                return null;

            return await dbSet.FindAsync(new object?[] { id }, cancellationToken);
        }

        /// <summary>
        /// Gets all Entities by Id List 
        /// </summary>
        /// <param name="ids">Id List</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            List<TEntity> entities = new ();

            foreach( var id in ids)
                entities.Add(await GetByIdAsync(id, cancellationToken));

            return entities;
        }

        /// <summary>
        /// Gets all Entities
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get an Entity by a key value
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByKeyValueAsync(object? keyValue, CancellationToken cancellationToken)
        {
            if (keyValue == null)
                return null;

            return await dbSet.FindAsync(new object?[] { keyValue }, cancellationToken);
        }
    }
}
