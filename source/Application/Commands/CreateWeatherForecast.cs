using Mediator;
using Source.Application.Interfaces;
using Source.Domain.Entities;

namespace Source.Application.Commands;

public class CreateWeatherForecastCommand(WeatherForecast weatherForecast) : IRequest<WeatherForecast>
{
    public WeatherForecast WeatherForecast { get; set; } = weatherForecast;
}

public class CreateWeatherForecastCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateWeatherForecastCommand, WeatherForecast>
{
    public async ValueTask<WeatherForecast> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        WeatherForecast createdWeatherForecast = await unitOfWork.WeatherForecasts.AddAsync(request.WeatherForecast, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return createdWeatherForecast;
    }
}

