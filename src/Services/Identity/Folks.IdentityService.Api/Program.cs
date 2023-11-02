using Serilog;

using Folks.IdentityService.Api.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureServices();

    // Required to fix the problem with css isolation in Razor runtime compilation
    builder.WebHost.UseStaticWebAssets();

    var app = builder.Build();
    app.ConfigurePipeline();
    app.Run();
}
catch (Exception exception) when (
    // https://github.com/dotnet/runtime/issues/60600
    exception.GetType().Name is not "StopTheHostException"
    // HostAbortedException was added in .NET 7, but since we target .NET 6 we
    // need to do it this way until we target .NET 8
    && exception.GetType().Name is not "HostAbortedException")
{
    Log.Fatal(exception, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

