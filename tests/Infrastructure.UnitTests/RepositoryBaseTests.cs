using System.Diagnostics;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Source.Domain.Entities;
using Source.Infrastructure.EntityFramework;
using Source.Infrastructure.EntityFramework.Repositories;
using Tests.Shared;
using Tests.Shared.CustomXunitTraits;

namespace Tests.Infrastructure.UnitTests;

[UnitTest]
public class RepositoryBaseTests
{
    private List<WeatherForecast>? _weatherForecasts;
    private readonly DbContextOptions<DatabaseContext> _contextOptions;
    private readonly Mock<ILogger<UnitOfWork>> _mockLogger = new();

    public RepositoryBaseTests()
    {
        //WeatherForecastGenerator.Generate(5);
        WeatherForecastGenerator.GenerateStaticData();
        _weatherForecasts = WeatherForecastGenerator.WeatherForecasts;

        _contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("RepositoryBaseTests")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging()
            .Options;

        using DatabaseContext context = new(_contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Debug.Assert(_weatherForecasts != null, nameof(_weatherForecasts) + " != null");
        context.AddRange(_weatherForecasts);

        context.SaveChanges();
    }

    [Fact, PositiveTestCase]
    public async Task GetAllAsync_Should_Return_All_Entities()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(10);
    }
    
    [Fact, PositiveTestCase]
    public async Task GetByIdAsync_Should_Return_Entity_By_Id()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        uint id = _weatherForecasts.First().Id;

        // Act
        WeatherForecast? result = await repository.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
    }
    
    [Fact, PositiveTestCase]
    public async Task GetWhereAsync_Should_Return_Entities_That_Match_Predicate()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);

        // Act
        var result = await repository.GetWhereAsync(wf => wf.City == "Pittsburgh");

        // Assert
        result.Should().HaveCount(1);
    }
    
    [Fact, PositiveTestCase]
    public async Task AddAsync_Should_Add_Entity()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        WeatherForecast newWeatherForecast = new()
        {
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            TempCelsius = 32,
            Summary = "Cloudy",
            City = "New York"
        };

        // Act
        WeatherForecast result = await repository.AddAsync(newWeatherForecast);
        await context.SaveChangesAsync();

        // Assert
        result.Id.Should().NotBe(0);
    }
    
    [Fact, PositiveTestCase]
    public async Task AddRangeAsync_Should_Add_Range_Of_Entities()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        var newWeatherForecasts = new List<WeatherForecast>
        {
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TempCelsius = 32,
                Summary = "Cloudy",
                City = "New York"
            },
            new()
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TempCelsius = 32,
                Summary = "Cloudy",
                City = "New York"
            }
        };

        // Act
        await repository.AddRangeAsync(newWeatherForecasts);
        await context.SaveChangesAsync();
        
        int count = await context.WeatherForecasts.CountAsync();

        // Assert
        count.Should().Be(12);
    }
    
    [Fact, PositiveTestCase]
    public async Task UpdateAsync_Should_Update_Entity()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        WeatherForecast weatherForecast = _weatherForecasts.First();
        weatherForecast.Summary = "Updated Summary";

        // Act
        await repository.UpdateAsync(weatherForecast);
        await context.SaveChangesAsync();

        WeatherForecast? updatedWeatherForecast = await repository.GetByIdAsync(weatherForecast.Id);

        // Assert
        updatedWeatherForecast.Should().NotBeNull();
        updatedWeatherForecast!.Summary.Should().Be("Updated Summary");
    }
    
    [Fact, PositiveTestCase]
    public async Task UpdateRangeAsync_Should_Update_Range_Of_Entities()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        var weatherForecasts = _weatherForecasts.Take(2).ToList();
        weatherForecasts.ForEach(wf => wf.Summary = "Updated Summary");

        // Act
        await repository.UpdateRangeAsync(weatherForecasts);
        await context.SaveChangesAsync();

        var updatedWeatherForecasts = await repository.GetWhereAsync(wf => wf.Summary == "Updated Summary");

        // Assert
        updatedWeatherForecasts.Should().HaveCount(2);
    }
    
    [Fact, PositiveTestCase]
    public async Task DeleteAsync_Should_Delete_Entity()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        WeatherForecast weatherForecast = _weatherForecasts.First();

        // Act
        await repository.DeleteAsync(weatherForecast.Id);
        await context.SaveChangesAsync();

        WeatherForecast? deletedWeatherForecast = await repository.GetByIdAsync(weatherForecast.Id);

        // Assert
        deletedWeatherForecast.Should().BeNull();
    }
    
    [Fact, PositiveTestCase]
    public async Task DeleteRangeAsync_Should_Delete_Range_Of_Entities()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        var weatherForecasts = _weatherForecasts.Take(2).ToList();

        // Act
        await repository.DeleteRangeAsync(weatherForecasts);
        await context.SaveChangesAsync();

        var deletedWeatherForecasts = await repository.GetWhereAsync(wf => wf.City == "Pittsburgh" || wf.City == "Chicago");

        // Assert
        deletedWeatherForecasts.Should().BeEmpty();
    }
    
    [Fact, PositiveTestCase]
    public async Task GetAllAsync_Should_Return_Empty_List_When_No_Entities_Exist()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        context.RemoveRange(context.WeatherForecasts);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact, NegativeTestCase]
    public async Task GetByIdAsync_Should_Return_Null_When_Entity_Does_Not_Exist()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        uint id = 0;

        // Act
        WeatherForecast? result = await repository.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
    
    [Fact, NegativeTestCase]
    public async Task GetWhereAsync_Should_Return_Empty_List_When_No_Entities_Match_Predicate()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);

        // Act
        var result = await repository.GetWhereAsync(wf => wf.City == "Amsterdam");

        // Assert
        result.Should().BeEmpty();
    }
    
    [Fact, NegativeTestCase]
    public async Task GetByIdAsync_Should_Return_Null_When_Entity_Does_Not_Exist_With_Invalid_Id()
    {
        // Arrange
        await using DatabaseContext context = new(_contextOptions);
        var repository = new RepositoryBase<WeatherForecast>(_mockLogger.Object, context);
        const uint id = 100;

        // Act
        WeatherForecast? result = await repository.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
    
    // TODO: Add test for AddAsync with invalid entity
    // TODO: Add test for AddAsync with existing entity
    // TODO: Add test for AddRangeAsync with invalid entities
    // TODO: Add test for AddRangeAsync with existing entities
}