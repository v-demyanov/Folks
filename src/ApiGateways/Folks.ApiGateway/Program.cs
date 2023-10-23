using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

// Add services to the container.
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.Run();
