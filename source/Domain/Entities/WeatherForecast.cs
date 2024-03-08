namespace Source.Domain.Entities;

public class WeatherForecast : EntityBase
{
    public DateOnly Date { get; set; }

    public string City { get; set; }
    public int TempCelsius { get; set; }

    public string? Summary { get; set; }
}