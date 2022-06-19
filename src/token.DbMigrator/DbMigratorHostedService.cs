using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace token.DbMigrator;

/// <summary>
/// 
/// </summary>
public class DbMigratorHostedService : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hostApplicationLifetime"></param>
    /// <param name="configuration"></param>
    public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var application = await AbpApplicationFactory.CreateAsync<TokenDbMigratorModule>(options =>
               {
                   options.Services.ReplaceConfiguration(_configuration);
                   options.UseAutofac();
                   options.Services.AddLogging(c => c.AddSerilog());
               }))
        {
            await application.InitializeAsync();

            await application.ShutdownAsync();

            _hostApplicationLifetime.StopApplication();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}