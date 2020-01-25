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

        //[HttpGet]
        //[Route("{id?}")]
        /*public List<Pelicula> Get(Pelicula peli)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();*/

        /*if (peli.numero > -1)
        {
            return peli.numero > Pelicula.Instance.numbers.Count
                ? new List<Pelicula>()
                : new List<Pelicula>() { Pelicula.Instance.weatherForecasts[peli.numero] };
        }
        return Pelicula.Instance.weatherForecasts;
    }

    [HttpPost]
    public Pelicula Agregar([FromBody]Pelicula pelicula)
    {
        Pelicula.Instance.weatherForecasts.Add(pelicula);
        return pelicula;
    }
    }*/
    }
}
