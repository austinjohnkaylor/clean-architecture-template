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

    /// <summary>
    /// Generates 10 manually-set <see cref="WeatherForecast"/>s and sets the <see cref="WeatherForecasts"/> property
    /// </summary>
    public static void GenerateStaticData()
    {
        WeatherForecasts = new List<WeatherForecast>();
        WeatherForecasts?.AddRange(new[]
        {
            new WeatherForecast()
            {
                Id = 1,
                City = "Pittsburgh",
                Date = new DateOnly(2021, 10, 1),
                TempCelsius = 15,
                Summary = Summaries[3],
                CreatedBy = "system",
                CreatedAt = new DateTime(2021, 10, 1)
            },
            new WeatherForecast
            {
                Id = 2,
                City = "Chicago",
                Date = new DateOnly(2021, 11, 5),
                TempCelsius = 20,
                Summary = Summaries[4],
                CreatedBy = "system",
                CreatedAt = new DateTime(2021, 11, 5)
            },
            new WeatherForecast
            {
                Id = 3,
                City = "New York",
                Date = new DateOnly(2022, 1, 1),
                TempCelsius = 25,
                Summary = Summaries[5],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 1, 1)
            },
            new WeatherForecast
            {
                Id = 4,
                City = "Los Angeles",
                Date = new DateOnly(2022, 2, 14),
                TempCelsius = 30,
                Summary = Summaries[6],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 2, 14)
            },
            new WeatherForecast
            {
                Id = 5,
                City = "Miami",
                Date = new DateOnly(2022, 3, 20),
                TempCelsius = 35,
                Summary = Summaries[7],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 3, 20)
            },
            new WeatherForecast
            {
                Id = 6,
                City = "San Francisco",
                Date = new DateOnly(2022, 4, 30),
                TempCelsius = 40,
                Summary = Summaries[8],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 4, 30)
            },
            new WeatherForecast
            {
                Id = 7,
                City = "Seattle",
                Date = new DateOnly(2022, 5, 15),
                TempCelsius = 45,
                Summary = Summaries[9],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 5, 15)
            },
            new WeatherForecast
            {
                Id = 8,
                City = "Denver",
                Date = new DateOnly(2022, 6, 1),
                TempCelsius = -5,
                Summary = Summaries[0],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 6, 1)
            },
            new WeatherForecast
            {
                Id = 9,
                City = "Phoenix",
                Date = new DateOnly(2022, 7, 4),
                TempCelsius = 5,
                Summary = Summaries[1],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 7, 4)
            },
            new WeatherForecast
            {
                Id = 10,
                City = "Las Vegas",
                Date = new DateOnly(2022, 8, 10),
                TempCelsius = 10,
                Summary = Summaries[2],
                CreatedBy = "system",
                CreatedAt = new DateTime(2022, 8, 10)
            }
        });
    }
}