using Consul;
using Serilog;
using Serilog.Events;
using token;
using token.HttpApi.Module;

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

builder.Services.AddSingleton(Log.Logger);

await builder.AddApplicationAsync<TokenWebModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
app.MapControllers();

RegisterConsul(app, app.Configuration, app.Lifetime);

await app.RunAsync();

void RegisterConsul(IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
{
    var configurationSection = configuration.GetSection(ConsulOption.Name);
    var consulOption = configurationSection.Get<ConsulOption>();

    var consulClient = new ConsulClient(x =>
    {
        x.Address = new Uri(consulOption.Address);
    });

    var registration = new AgentServiceRegistration()
    {
        ID = Guid.NewGuid().ToString(),
        Name = consulOption.ServiceName,
        Address = consulOption.ServiceIP,
        Port = consulOption.ServicePort,
        Check = new AgentCheckRegistration()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
            Interval = TimeSpan.FromSeconds(10),
            HTTP = consulOption.ServiceHealthCheck,
            Timeout = TimeSpan.FromSeconds(5)
        }
    };

    consulClient.Agent.ServiceRegister(registration).Wait();
    lifetime.ApplicationStopping.Register(() =>
    {
        consulClient.Agent.ServiceDeregister(registration.ID).Wait();
    });
}