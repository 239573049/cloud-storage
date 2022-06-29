using Consul;
using Serilog;
using Serilog.Events;
using token;
using Winton.Extensions.Configuration.Consul;
using Winton.Extensions.Configuration.Consul.Parsers;

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

var consul = Environment.GetEnvironmentVariable("consul") ?? "http://tokengo.top:8500";
builder.Configuration
    .AddConsul("token", options =>
    {
        options.Parser = new SimpleConfigurationParser();
        options.ConsulConfigurationOptions = cco =>
        {
            cco.Address = new Uri(consul);
            options.Optional = true;
            options.ReloadOnChange = true;
            options.OnLoadException = exception =>
            {
                Console.WriteLine(exception.Exception.Message);
            };
            options.ConvertConsulKVPairToConfig = kvPair =>
            {
                var normalizedKey = kvPair.Key
                    .Replace("token/", string.Empty)
                    .Replace("__", "/")
                    .Replace("/", ":")
                    .Trim('/');

                using Stream valueStream = new MemoryStream(kvPair.Value);
                using var streamReader = new StreamReader(valueStream);
                var parsedValue = streamReader.ReadToEnd();

                return new Dictionary<string, string>()
                {
                { normalizedKey, parsedValue }
                };
            };
        };
        options.ReloadOnChange = true;
    }).Build();


builder.Services.AddSingleton(Log.Logger);

await builder.AddApplicationAsync<TokenWebModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
app.MapControllers();

// 在环境变量获取当前服务的服务名称
// var serviceName = Environment.GetEnvironmentVariable("ServiceName");
// var servicePort = Environment.GetEnvironmentVariable("ServicePort");

RegisterConsul(app, app.Configuration, app.Lifetime);

await app.RunAsync();
void RegisterConsul(IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
{
    var consulClient = new ConsulClient(x =>
    {
        x.Address = new Uri(consul);
    });

    var registration = new AgentServiceRegistration()
    {
        ID = Guid.NewGuid().ToString(),
        Name = "consulOption.ServiceName",
        Address = "http://tokengo.top:8000",
        Port = 80,
        Check = new AgentCheckRegistration()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(100),
            Interval = TimeSpan.FromSeconds(100),
            HTTP = "http://tokengo.top:18888/health",
            Timeout = TimeSpan.FromSeconds(100)
        }
    };

    consulClient.Agent.ServiceRegister(registration).Wait();
    lifetime.ApplicationStopping.Register(() =>
    {
        consulClient.Agent.ServiceDeregister(registration.ID).Wait();
    });
}