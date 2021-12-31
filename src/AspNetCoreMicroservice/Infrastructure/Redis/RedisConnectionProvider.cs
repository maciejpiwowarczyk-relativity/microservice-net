using System.Text;
using StackExchange.Redis;

namespace AspNetCoreMicroservice.Infrastructure.Redis
{
	public static class RedisConnectionProvider
	{
		public static IConnectionMultiplexer Connection { get; set; }
		
		public static void Initialize(IConfiguration configuration)
		{
			var sb = new StringBuilder();
			using var tw = new StringWriter(sb);
			try
			{
				Connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"], tw);
			}
			catch (Exception)
			{
				tw.Flush();
				Console.WriteLine(sb.ToString());
				throw;
			}
		}
	}
}
