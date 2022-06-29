using Serilog;
using Serilog.Events;
using token.SignalR.Web;

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
    .MinimumLevel.Error()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json").Build())
    .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/log/", "log"), rollingInterval: RollingInterval.Day))
    .CreateLogger();

Log.Information("Signalr...");
var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac()
    .UseSerilog();

builder.Services.AddSingleton(Log.Logger);

await builder.AddApplicationAsync<TokenSignalRWebModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
app.MapControllers();

await app.RunAsync();
