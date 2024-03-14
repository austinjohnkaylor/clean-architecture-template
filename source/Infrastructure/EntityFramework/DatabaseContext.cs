using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Source.Domain;
using Source.Domain.Entities;

namespace Source.Infrastructure.EntityFramework;

/// <summary>
/// An Entity Framework Database Context for the application.
/// </summary>
public class DatabaseContext : DbContext
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public DatabaseContext()
    {
        
    }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<WeatherForecast>().HasBaseType<EntityBase>();
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}