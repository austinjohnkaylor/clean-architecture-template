using Mediator;
using Microsoft.AspNetCore.Mvc;
using Source.API.Models;
using Source.Application.Queries;
using WeatherForecast = Source.Domain.Entities.WeatherForecast;

namespace Source.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
    : ControllerBase
{
    
    [HttpGet(Name = "GetAllWeatherForecasts")]
    public async Task<IEnumerable<WeatherForecast>> GetAll()
    {
        logger.LogInformation("Getting weather forecasts");
        var result = await mediator.Send(new GetAllWeatherForecastsQuery());
        var weatherForecasts = result.ToList();
        logger.LogInformation("Got {count} weather forecasts", weatherForecasts.Count());
        return weatherForecasts;
    }
}
