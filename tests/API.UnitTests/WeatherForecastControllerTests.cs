using FluentAssertions;
using Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using Source.API.Controllers;
using Source.Application.Queries;
using Source.Domain.Entities;
using Tests.Shared.CustomXunitTraits;

namespace Tests.API.UnitTests;

[UnitTest]
public class WeatherForecastControllerTests
{
    private readonly WeatherForecast[] _weatherForecasts =
    [
        new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TempCelsius = 25, Summary = "Hot" },
        new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Now), TempCelsius = 10, Summary = "Cold" }
    ];

    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Mock<ILogger<WeatherForecastController>> _mockLogger = new();
    
    [Fact]
    [PositiveTestCase]
    public async Task GetAll_ReturnsWeatherForecasts_WhenWeatherForecastsExist()
    {
        // Arrange
        _mockMediator
            .Setup(expression: m => m.Send(It.IsAny<GetAllWeatherForecastsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_weatherForecasts);

        WeatherForecastController controller = new(logger: _mockLogger.Object, mediator: _mockMediator.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectation: _weatherForecasts);
    }

    [Fact]
    [PositiveTestCase]
    public async Task GetAll_ReturnsEmptyList_WhenNoWeatherForecastsExist()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<GetAllWeatherForecastsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<WeatherForecast>());

        WeatherForecastController controller = new(_mockLogger.Object, _mockMediator.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().BeEmpty();
    }
}