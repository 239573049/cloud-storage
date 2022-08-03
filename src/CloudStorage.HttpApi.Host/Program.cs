using CloudStorage.Domain.Shared;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;
using token;
using token.Hubs;

Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
    .MinimumLevel.Error()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json")
        .AddJsonFile("appsettings.Development.json").Build())
    .WriteTo.Async(c => c.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/log/", "log"),
        rollingInterval: RollingInterval.Day))
    .WriteTo.Async(c => c.Console())
    .CreateLogger();

Log.Information("管理服务启动...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac()
    .UseSerilog();

await builder.AddApplicationAsync<CloudStorageHttpApiHostModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();

app.UseStaticFiles();

app.MapControllers();

app.MapHub<FileStreamHub>(SignalRConstants.FileStream, x =>
{
});

await app.RunAsync();