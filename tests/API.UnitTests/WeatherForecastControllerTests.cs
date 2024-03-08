using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Source.API.Controllers;
using Tests.Shared.CustomXunitTraits;

namespace Tests.API.UnitTests;

[UnitTest]
public class WeatherForecastControllerTests
{
    private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;

    public WeatherForecastControllerTests()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
    }
    
    [Fact]
    [Trait("Category","Get")]
    public void Get_Should_Return_WeatherForecasts()
    {
        // Arrange
        WeatherForecastController controller = new(_loggerMock.Object);
        
        // Act
        var result = controller.Get();
        
        // Assert
        result.Should().NotBeEmpty();
        result.Count().Should().Be(5);
    }
}