using Bogus;
using Source.Domain.Entities;

namespace Tests.Shared;

/// <summary>
/// Generates <see cref="WeatherForecast"/>s for testing purposes using the <see cref="Bogus"/> library
/// </summary>
public static class WeatherForecastGenerator
{
    private static uint _id = 1;
    public static List<WeatherForecast>? WeatherForecasts { get; set; }
    private static Faker? _f;
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    /// <summary>
    /// Generates a specified number of <see cref="WeatherForecast"/> using a <see cref="Faker"/> instance
    /// </summary>
    /// <param name="numberOfEntitiesToGenerate">The number of <see cref="WeatherForecast"/> to generate</param>
    public static void Generate(int numberOfEntitiesToGenerate)
    {
        _f = new Faker();
        WeatherForecasts = new List<WeatherForecast>();

        for (var i = 0; i < numberOfEntitiesToGenerate; i++)
        {
            WeatherForecast entity = new()
            {
                Id = _id++,
                Date = _f.Date.FutureDateOnly(0, DateOnly.FromDateTime(DateTime.Now)),
                TempCelsius = _f.Random.Number(0, 100),
                Summary = _f.Random.ArrayElement(Summaries),
                City = _f.Address.City(),
                CreatedBy = "system",
                CreatedAt = _f.Date.Past(1, DateTime.Now)
            };
            WeatherForecasts.Add(entity);
        }
    }
}