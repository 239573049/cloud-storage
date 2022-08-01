using CloudStorage.Application;
using CloudStorage.Domain.Shared;
using CloudStorage.EntityFrameworkCore;
using CloudStorage.HttpApi;
using CloudStorage.HttpApi.Module;
using Microsoft.Extensions.FileProviders;
using token.Hubs;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using FileStreamOptions = CloudStorage.Domain.Shared.Options.FileStreamOptions;

namespace token;

/// <summary>
/// web
/// </summary>
[DependsOn(
    typeof(CloudStorage.HttpApi.CloudStorageHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(CloudStorageApplicationModule),
    typeof(CloudStorageEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class CloudStorageHttpApiModule : AbpModule
{
    private void ConfigConsul(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(nameof(ConsulOption));
        context.Services.Configure<ConsulOption>(configurationSection);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigConsul(context, context.Services.GetConfiguration());
        ConfigureFileStreamOptions(context.Services);
        context.Services.AddHealthChecks();
    }

    /// <summary>
    /// FileStream配置
    /// </summary>
    /// <param name="services"></param>
    private void ConfigureFileStreamOptions(IServiceCollection services)
    {
        var configurationSection = services.GetConfiguration().GetSection(FileStreamOptions.Name);
        services.Configure<FileStreamOptions>(configurationSection);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseHealthChecks("/health");

        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}