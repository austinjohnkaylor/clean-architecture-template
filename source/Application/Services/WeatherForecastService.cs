using Source.Application.Interfaces;
using Source.Domain.Entities;

namespace Source.Application.Services;

public class WeatherForecastService : IWeatherForecastService
{
    public WeatherForecastService(IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(unitOfWork);
        _unitOfWork = unitOfWork;
    }
    
    public Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
    {
        return _unitOfWork.WeatherForecasts.GetAllAsync();
    }
    
    public Task<WeatherForecast> GetForecastAsync(uint id)
    {
        return _unitOfWork.WeatherForecasts.GetByIdAsync(id);
    }
    
    public Task<WeatherForecast> CreateForecastAsync(WeatherForecast forecast)
    {
        return _unitOfWork.WeatherForecasts.AddAsync(forecast);
    }
    
    public Task<WeatherForecast> UpdateForecastAsync(WeatherForecast forecast)
    {
        return _unitOfWork.WeatherForecasts.UpdateAsync(forecast) as Task<WeatherForecast>;
    }
    
    public Task DeleteForecastAsync(uint id)
    {
        return _unitOfWork.WeatherForecasts.DeleteAsync(id);
    }
    
    private readonly IUnitOfWork _unitOfWork;
}