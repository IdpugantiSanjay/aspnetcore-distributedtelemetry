using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using Plutus.Api.Common;

namespace Plutus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            using (var activity = AppActivitySource.Instance.StartActivity("WeatherForecast"))
            {
                _logger.LogInformation(activity is null ? "Activity is null" : "Activity is there");
                activity?.SetTag("temperatureInCelsius", "36c");
                activity?.SetTag("summary", "Cool");
            }
            
            return Enumerable.Range(1, 5).Select(index =>
                {
                    var temperatureC = rng.Next(-20, 55);
                    var summary = Summaries[rng.Next(Summaries.Length)];
                    return new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = temperatureC,
                        Summary = summary
                    };
                })
                .ToArray();
        }
    }
}