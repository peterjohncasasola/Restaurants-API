using Microsoft.AspNetCore.Mvc; 
using Restaurant.API;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _weatherForecastService.Get();
        return result;
    }

    [HttpGet("first")]
    public WeatherForecast GetFirstWeatherForecast()
    {
        var result = _weatherForecastService.Get().First();
        return result;
    }
}
