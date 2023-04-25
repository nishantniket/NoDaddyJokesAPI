using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NoDaddyJokesNN.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //Method to get Count of the available jokes
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/joke/count"),
                Headers =
                {
                    { "X-RapidAPI-Key", "7e3feecae3msh50ef0108f737716p1f0792jsn1e088bdee6b8" },
                    { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Ok(new { success = true, data = body });
            }
        }

        //Method to get Random Jokes
        [HttpGet]
        public async Task<IActionResult> GetRandomJokes()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/random/joke"),
                Headers =
                {
                    { "X-RapidAPI-Key", "7e3feecae3msh50ef0108f737716p1f0792jsn1e088bdee6b8" },
                    { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return Ok(new { success = true, data = body });
            }

        }
    }
}
