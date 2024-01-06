using Serilog;

using Folks.ChannelsService.Api.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

app.Run();
