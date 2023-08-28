using Microsoft.AspNetCore.Mvc;
using ownMineManager;

namespace ownMineNET.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    

    public WeatherForecastController() { }

    [HttpGet(Name = "GetWeatherForecast")]
    public bool Get() => Deploy.PortAvailable(5164);
}