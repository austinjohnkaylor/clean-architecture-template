using Source.Domain.Entities;

namespace Source.Application.Interfaces;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetForecastsAsync();
    Task<WeatherForecast> GetForecastAsync(uint id);
    Task<WeatherForecast> CreateForecastAsync(WeatherForecast forecast);
    Task<WeatherForecast> UpdateForecastAsync(WeatherForecast forecast);
    Task DeleteForecastAsync(uint id);
}