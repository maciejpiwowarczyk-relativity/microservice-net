﻿using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace AspNetCoreMicroservice.Infrastructure.OpenTelemetry
{
	public static class OpenTelemetryServiceCollectionExtensions
	{
		public static void AddOpenTelemetry(this IServiceCollection services)
		{
			// Adding the OtlpExporter creates a GrpcChannel.
			// This switch must be set before creating a GrpcChannel/HttpClient when calling an insecure gRPC service.
			// See: https://docs.microsoft.com/aspnet/core/grpc/troubleshoot#call-insecure-grpc-services-with-net-core-client
			AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

			services.AddOpenTelemetryTracing(builder =>
			{
				builder
					.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("demo-micro-service"))
					.AddAspNetCoreInstrumentation(options =>
					{
						options.RecordException = true;
						options.Filter = context => context.Request.Path.ToString().Contains("/api/");
					})
					.AddHttpClientInstrumentation(options =>
					{
						options.RecordException = true;
					})
					.AddOtlpExporter(config =>
					{
						config.Endpoint = new Uri("http://localhost:4317");
					});
				
				builder.AddConsoleExporter();
			});
		}
	}
}
