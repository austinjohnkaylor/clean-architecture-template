using Source.Domain.Entities;

namespace Source.Application.Interfaces;

/// <summary>
/// An interface for the Unit of Work pattern
/// </summary>
/// <remarks>https://code-maze.com/csharp-unit-of-work-pattern/</remarks>
public interface IUnitOfWork
{
    IRepositoryBase<WeatherForecast> WeatherForecasts { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}