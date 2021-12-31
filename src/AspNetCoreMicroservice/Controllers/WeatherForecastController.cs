using AspNetCoreMicroservice.Infrastructure.Redis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AspNetCoreMicroservice.Controllers
{
	[ApiController]
	[Route("api/weather-forecast")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private static readonly RedisKey RedisKey = new RedisKey("WeatherForecast");

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet("latest")]
		public IEnumerable<WeatherForecast> Get()
		{
			return GetForecast();
		}

		private static WeatherForecast[] GetForecast()
		{
			var db = RedisConnectionProvider.Connection.GetDatabase();
			var forecastValue = db?.StringGet(RedisKey);
			if (forecastValue.HasValue)
			{
				return JsonConvert.DeserializeObject<WeatherForecast[]>((string)forecastValue) ?? Array.Empty<WeatherForecast>();
			}

			var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
				{
					Date = DateTime.Now.AddDays(index),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				})
				.ToArray();
			db?.StringSet(RedisKey, JsonConvert.SerializeObject(forecast));
			return forecast;
		}
	}
}