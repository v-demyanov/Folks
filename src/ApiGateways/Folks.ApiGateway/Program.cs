using Folks.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.ConfigureServices();

var app = builder.Build();

await app.ConfigurePipelineAsync();

app.Run();
