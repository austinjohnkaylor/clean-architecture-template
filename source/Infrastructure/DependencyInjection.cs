using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Source.Application.Interfaces;
using Source.Infrastructure.EntityFramework;
using Source.Infrastructure.EntityFramework.Repositories;

namespace Source.Infrastructure;

/// <summary>
/// A class for injecting the Infrastructure Layer's services into microsoft's service collection 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all dependencies for the Infrastructure layer.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString">The connection string for the SQL Server</param>
    /// <returns></returns>
    public static void AddInfrastructureLayer(this IServiceCollection services, string? connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
        });
        
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>)); // Register all the repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>(); // must be after repositories
    }
}