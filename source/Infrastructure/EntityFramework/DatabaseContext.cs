using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Source.Infrastructure.EntityFramework;

/// <summary>
/// An Entity Framework Database Context for the application.
/// </summary>
public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        
    }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : base(contextOptions)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}