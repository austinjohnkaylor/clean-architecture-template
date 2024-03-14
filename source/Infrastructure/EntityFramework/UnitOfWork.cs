using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Source.Application.Interfaces;
using Source.Domain.Entities;
using Source.Infrastructure.EntityFramework.Repositories;

namespace Source.Infrastructure.EntityFramework;

/// <summary>
/// A class for managing the entity framework database context and transactions for the application using the unit-of-work pattern.
/// </summary>
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly DatabaseContext _context;

    /// <summary>
    /// A class for managing the entity framework database context and transactions for the application using the unit-of-work pattern.
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="context">The entity framework database context</param>
    public UnitOfWork(ILogger<UnitOfWork> logger, DatabaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IRepositoryBase<WeatherForecast> WeatherForecasts => new RepositoryBase<WeatherForecast>(_logger, _context);

    /// <summary>
    /// Save changes to the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Saving changes");
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begin a new transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Beginning transaction");
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commit the current transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Committing transaction");
        await _transaction.CommitAsync(cancellationToken);
    }

    /// <summary>
    /// Rollback the current transaction.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Rolling back transaction");
        await _transaction.RollbackAsync(cancellationToken);
    }

    /// <summary>
    /// Dispose of all the resources being used by the UnitOfWork.
    /// </summary>
    public void Dispose()
    {
        _logger.LogInformation("Disposing UnitOfWork resources");
        GC.SuppressFinalize(this);
        _transaction.Dispose();
        _context.Dispose();
    }
}