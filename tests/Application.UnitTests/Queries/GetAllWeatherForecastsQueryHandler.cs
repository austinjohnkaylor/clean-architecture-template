using FluentAssertions;
using Moq;
using Source.Application.Queries;
using Source.Domain.Entities;
using Source.Application.Interfaces;
using Tests.Shared.CustomXunitTraits;

namespace Tests.Application.UnitTests.Queries;

[UnitTest]
public class GetAllWeatherForecastsQueryHandlerTests
{
    [Fact]
    [PositiveTestCase]
    public async Task Handle_ReturnsWeatherForecasts_WhenWeatherForecastsExist()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var weatherForecasts = new List<WeatherForecast> { new WeatherForecast(), new WeatherForecast() };
        mockUnitOfWork.Setup(u => u.WeatherForecasts.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(weatherForecasts);

        var handler = new GetAllWeatherForecastsQueryHandler(mockUnitOfWork.Object);

        // Act
        var result = await handler.Handle(new GetAllWeatherForecastsQuery(), default);

        // Assert
        Assert.Equal(weatherForecasts, result);
    }

    [Fact]
    [PositiveTestCase]
    public async Task Handle_ReturnsEmptyList_WhenNoWeatherForecastsExist()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var weatherForecasts = new List<WeatherForecast>();
        mockUnitOfWork.Setup(u => u.WeatherForecasts.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(weatherForecasts);

        var handler = new GetAllWeatherForecastsQueryHandler(mockUnitOfWork.Object);

        // Act
        var result = await handler.Handle(new GetAllWeatherForecastsQuery(), default);

        // Assert
        Assert.Empty(result);
    }
    
    [Fact]
    [NegativeTestCase]
    public async Task Handle_ThrowsException_WhenUnitOfWorkIsNull()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;
        GetAllWeatherForecastsQueryHandler handler = new(nullUnitOfWork);

        // Act & Assert
        Func<Task> f = async () => { await handler.Handle(new GetAllWeatherForecastsQuery(), default); };
        await f.Should().ThrowAsync<NullReferenceException>();
    }

    [Fact]
    [NegativeTestCase]
    public async Task Handle_ThrowsException_WhenUnitOfWorkThrowsException()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.WeatherForecasts.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        var handler = new GetAllWeatherForecastsQueryHandler(mockUnitOfWork.Object);

        // Act & Assert
        Func<Task> f = async () => { await handler.Handle(new GetAllWeatherForecastsQuery(), default); };
        await f.Should().ThrowAsync<Exception>();
    }
}