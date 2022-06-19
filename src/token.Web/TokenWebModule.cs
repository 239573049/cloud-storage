using Autofac.Extensions.DependencyInjection;
using Consul;
using token.Application;
using token.EntityFrameworkCore;
using token.HttpApi;
using token.HttpApi.Module;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace token;

[DependsOn(
    typeof(TokenHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(TokenApplicationModule),
    typeof(TokenEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class TokenWebModule : AbpModule
{
    private void ConfigConsul(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection(nameof(ConsulOption));
        context.Services.Configure<ConsulOption>(configurationSection);
        
    }
    
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseHealthChecks("/health");
        
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
        
    }

}