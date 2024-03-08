using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using Source.Domain;
using Tests.Shared.CustomXunitTraits;

namespace Tests.ArchitectureTests;

[ArchitectureTest]
public class ApiTests
{
    private readonly Types _types = Types.InAssembly(typeof(Source.Infrastructure.DependencyInjection).Assembly);
    private readonly string _domain = Assembly.GetAssembly(typeof(EntityBase))?.GetName().Name ?? throw new InvalidOperationException("Domain assembly is null");
    private readonly string _application = Assembly.GetAssembly(typeof(Source.Application.DependencyInjection))?.GetName().Name ?? throw new InvalidOperationException("Application assembly is null");
    private readonly string _infrastructure = Assembly.GetAssembly(typeof(Source.Infrastructure.DependencyInjection))?.GetName().Name ?? throw new InvalidOperationException("Infrastructure assembly is null");
    private readonly string _api = Assembly.GetAssembly(typeof(Source.API.Program))?.GetName().Name ?? throw new InvalidOperationException("API assembly is null");
    
    [Fact]
    public void Api_Should_Have_Dependency_On_Application()
    {
        _types.That()
            .ResideInNamespace(_api)
            .Should()
            .HaveDependencyOn(_application)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Api_Should_Have_Dependency_On_Infrastructure()
    {
        _types.That()
            .ResideInNamespace(_api)
            .Should()
            .HaveDependencyOn(_infrastructure)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Api_Should_Not_Have_Dependency_On_Domain()
    {
        _types.That()
            .ResideInNamespace(_api)
            .ShouldNot()
            .HaveDependencyOn(_domain)
            .GetResult()
            .IsSuccessful.Should().BeTrue();
    }
}