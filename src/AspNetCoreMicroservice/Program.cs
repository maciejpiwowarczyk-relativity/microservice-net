using AspNetCoreMicroservice.Infrastructure.OpenTelemetry;
using AspNetCoreMicroservice.Infrastructure.Redis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RedisConnectionProvider.Initialize(configuration);
builder.Services.AddOpenTelemetry(configuration, RedisConnectionProvider.Connection);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
