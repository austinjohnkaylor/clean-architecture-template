using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Tests.Shared.CustomXunitTraits;

namespace Tests.API.IntegrationTests;

[IntegrationTest]
public class WeatherForecastControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>> // TODO: Figure out why this isn't finding Program in Source.API
{
    [Fact]
    public async Task Get_ReturnsWeatherForecast()
    {
        // Arrange
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.GetAsync("/WeatherForecast");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        string responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("Freezing", responseString);
    }
}
