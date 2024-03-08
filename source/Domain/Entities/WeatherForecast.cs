namespace Source.Domain.Entities;

public class WeatherForecast : EntityBase
{
    public DateOnly Date { get; set; }

    public string City { get; set; }
    public int TempCelsius { get; set; }

    public int TempFahrenheit => 32 + (int)(TempCelsius / 0.5556);

    public string? Summary { get; set; }
}