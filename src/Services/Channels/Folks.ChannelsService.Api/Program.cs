// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Api.Extensions;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.ConfigurePipeline();

app.Run();
