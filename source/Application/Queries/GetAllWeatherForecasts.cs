using Mediator;
using Source.Application.Interfaces;
using Source.Domain.Entities;

namespace Source.Application.Queries;

public class GetAllWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>;

public class GetAllWeatherForecastsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllWeatherForecastsQuery, IEnumerable<WeatherForecast>>
{
    public async ValueTask<IEnumerable<WeatherForecast>> Handle(GetAllWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.WeatherForecasts.GetAllAsync(cancellationToken);
    }
}
