using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Laboratorio00_LesterGarcia_1003115.Models;

namespace Laboratorio00_LesterGarcia_1003115.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       /* private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }*/

        [HttpGet]
        [Route("{id?}")]
        public IEnumerable<WeatherForecast> Get(int id = -1)
        {
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/

            if (id > -1)
            {
                return id > Pelicula.Instance.numbers.Count
                    ? new List<WeatherForecast>()
                    : new List<WeatherForecast>() { Pelicula.Instance.weatherForecasts[id] };
            }
            return Pelicula.Instance.weatherForecasts;
        }

        [HttpPost]
        public WeatherForecast Agregar([FromBody]WeatherForecast weatherForecast)
        {
            Pelicula.Instance.weatherForecasts.Add(weatherForecast);
            return weatherForecast;
        }
    }
}
