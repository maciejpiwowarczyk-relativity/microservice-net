using AspNetCoreMicroservice.Controllers;
using AspNetCoreMicroservice.Infrastructure.Redis;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using StackExchange.Redis;

namespace AspNetCoreMicroservice.Tests
{
	[TestFixture]
	public class WeatherForecastControllerTests
	{
		private WeatherForecastController _sut;
		[SetUp]
		public void Setup()
		{
			RedisConnectionProvider.Connection = new Mock<IConnectionMultiplexer>().Object;
			_sut = new WeatherForecastController(new NullLogger<WeatherForecastController>());
		}

		[Test]
		public void GetShouldNotReturnEmptyResult()
		{
			_sut.Get().Should().NotBeEmpty();
		}
	}
}