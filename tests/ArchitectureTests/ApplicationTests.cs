using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Source.Domain;
using Tests.Shared.CustomXunitTraits;

namespace Tests.ArchitectureTests;

[ArchitectureTest]
public class ApplicationTests
{
    private readonly Types _types = Types.InAssembly(typeof(Source.Application.DependencyInjection).Assembly);
    private readonly string _domain = Assembly.GetAssembly(typeof(EntityBase))?.GetName().Name ?? throw new InvalidOperationException("Domain assembly is null");
    private readonly string _application = Assembly.GetAssembly(typeof(Source.Application.DependencyInjection))?.GetName().Name ?? throw new InvalidOperationException("Application assembly is null");
    private readonly string _infrastructure = Assembly.GetAssembly(typeof(Source.Infrastructure.DependencyInjection))?.GetName().Name ?? throw new InvalidOperationException("Infrastructure assembly is null");
    private readonly string _api = Assembly.GetAssembly(typeof(Source.API.Program))?.GetName().Name ?? throw new InvalidOperationException("API assembly is null");
    
    [Fact]
    public void Application_Should_Have_Dependency_On_Domain()
    {
        _types.That()
            .ResideInNamespace(_application)
            .Should()
            .HaveDependencyOn(_domain)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Infrastructure()
    {
        _types.That()
            .ResideInNamespace(_application)
            .Should()
            .NotHaveDependencyOn(_infrastructure)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_Have_Dependency_On_API()
    {
        _types.That()
            .ResideInNamespace(_application)
            .Should()
            .NotHaveDependencyOn(_api)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
}