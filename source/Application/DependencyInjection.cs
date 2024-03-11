using Microsoft.Extensions.DependencyInjection;

namespace Source.Application;

/// <summary>
/// A class for injecting the Application Layer's services into microsoft's service collection 
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all dependencies for the Application layer.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();
    }
}