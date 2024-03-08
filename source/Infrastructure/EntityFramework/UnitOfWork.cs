using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Source.Application.Interfaces;

namespace Source.Infrastructure.EntityFramework;

/// <summary>
/// A class for managing the entity framework database context and transactions for the application using the unit-of-work pattern.
/// </summary>
/// <param name="logger">The logger</param>
/// <param name="context">The entity framework database context</param>
public class UnitOfWork(ILogger<UnitOfWork> logger, DatabaseContext context)
    : IUnitOfWork, IDisposable
{
    private IDbContextTransaction _transaction;
    
    /// <summary>
    /// Save changes to the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Saving changes");
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begin a new transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Beginning transaction");
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commit the current transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Committing transaction");
        await _transaction.CommitAsync(cancellationToken);
    }

    /// <summary>
    /// Rollback the current transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Rolling back transaction");
        await _transaction.RollbackAsync(cancellationToken);
    }

    /// <summary>
    /// Dispose of all the resources being used by the UnitOfWork.
    /// </summary>
    public void Dispose()
    {
        logger.LogInformation("Disposing UnitOfWork resources");
        GC.SuppressFinalize(this);
        _transaction.Dispose();
        context.Dispose();
    }
}