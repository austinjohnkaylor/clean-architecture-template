using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Source.Application.Interfaces;
using Source.Domain;

namespace Source.Infrastructure.EntityFramework.Repositories;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class RepositoryBase<T>(ILogger<UnitOfWork> logger, DatabaseContext databaseContext)
    : IRepositoryBase<T>
    where T : EntityBase, new()
{
    private readonly DbSet<T> _entity = databaseContext.Set<T>();

    /// <summary>
    /// Gets all entities of type T.
    /// </summary>
    /// <returns>All entities of type T</returns>
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting all {entity}s.", typeof(T).Name);
        return await _entity.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Gets an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The entity that has the unique identifier or an empty entity if no entity is found with the unique identifier</returns>
    public async Task<T?> GetByIdAsync(uint id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting {entity} by id {id}.", typeof(T).Name, id);
        T? result = await _entity.FindAsync([id], cancellationToken: cancellationToken);
        return result ?? null;
    }

    /// <summary>
    /// Gets a list of entities that match the predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A list of entities that match the predicate</returns>
    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting a list of {entity}s by {predicate}.", typeof(T).Name, predicate.Body.ToString());
        var result = await _entity.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
        return result;
    }

    /// <summary>
    /// Adds an entity of type T to the database.
    /// </summary>
    /// <param name="entity">The entity of type T</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The entity with a new unique identifier</returns>
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        SetCreatedFields(entity);
        
        logger.LogInformation("Adding {entity}.", typeof(T).Name);
        var result = await _entity.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Adds a range of entities of type T to the database.
    /// </summary>
    /// <param name="entities">The list of new entities of type T</param>
    /// <param name="cancellationToken"></param>
    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var listOfEntities = entities.ToList();
        foreach (T entity in listOfEntities)
            SetCreatedFields(entity);
        
        logger.LogInformation("Adding {count} new {entity}s.", listOfEntities.Count, typeof(T).Name);
        await _entity.AddRangeAsync(listOfEntities, cancellationToken);
    }

    /// <summary>
    /// Updates an entity of type T in the database.
    /// </summary>
    /// <param name="entity">The entity of type T</param>
    /// <param name="cancellationToken"></param>
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _entity.Attach(entity);
        _entity.Entry(entity).State = EntityState.Modified;
        
        SetUpdatedFields(entity);
        logger.LogInformation("Updating {entity}.", typeof(T).Name);

        _entity.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Updates a range of entities of type T in the database.
    /// </summary>
    /// <param name="entities">The list of entities of type T to update</param>
    /// <param name="cancellationToken"></param>
    public Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var listOfEntities = entities.ToList();
        
        foreach (T entity in listOfEntities)
        {
            SetUpdatedFields(entity);
            _entity.Attach(entity);
            _entity.Entry(entity).State = EntityState.Modified;
        }
        
        logger.LogInformation("Updating {count} {entity}s.", listOfEntities.Count, typeof(T).Name);
        _entity.UpdateRange(listOfEntities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes an entity of type T from the database.
    /// </summary>
    /// <param name="id">The id of entity of type T to delete</param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteAsync(uint id, CancellationToken cancellationToken = default)
    {
        T? result = await _entity.FindAsync([id], cancellationToken);
        
        if(result == null)
        {
            logger.LogWarning("{entity} with id {id} not found.", typeof(T).Name, id);
            return;
        }
        
        logger.LogInformation("Hard deleting {entity}.", typeof(T).Name);
        SetDeletedFields(result);
        _entity.Remove(result);
    }

    /// <summary>
    /// Deletes a range of entities of type T from the database.
    /// </summary>
    /// <param name="entities">The entity of type T to delete</param>
    /// <param name="cancellationToken"></param>
    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (T entity in entities)
        {
            T? result = await _entity.FindAsync([entity.Id], cancellationToken);
            if (result == null)
            {
                logger.LogWarning("{entity} with id {id} not found.", typeof(T).Name, entity.Id);
                continue;
            }
            logger.LogInformation("Hard deleting {entity}.", typeof(T).Name);
            SetDeletedFields(entity);
            _entity.Remove(entity);
        }
    }

    #region Local Private Methods

    /// <summary>
    /// Sets the created fields, CreatedAt and CreatedBy (if null), for an entity.
    /// </summary>
    /// <param name="entity">The entity of type T</param>
    private static void SetCreatedFields(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        if(string.IsNullOrEmpty(entity.CreatedBy))
            entity.CreatedBy = "system";
    }
    
    /// <summary>
    /// Sets the updated fields, UpdatedAt and UpdatedBy (if null), for an entity.
    /// </summary>
    /// <param name="entity">The entity of type T</param>
    private static void SetUpdatedFields(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        if(string.IsNullOrEmpty(entity.UpdatedBy))
            entity.UpdatedBy = "system";
    }
    
    /// <summary>
    /// Sets the deleted fields, DeletedAt and DeletedBy (if null), for an entity.
    /// </summary>
    /// <param name="entity">The entity of type T</param>
    private static void SetDeletedFields(T entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        if(string.IsNullOrEmpty(entity.DeletedBy))
            entity.DeletedBy = "system";
    }
    
    #endregion
}