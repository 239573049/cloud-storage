using token.Application;
using token.Domain.Shared;
using token.EntityFrameworkCore;
using token.HttpApi;
using token.SignalR.Web.Hubs;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace token.SignalR.Web;

[DependsOn(
    typeof(TokenHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(TokenEntityFrameworkCoreModule),
    typeof(TokenApplicationModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class TokenSignalRWebModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;

        services.AddSignalR();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseEndpoints(x =>
        {
            x.MapHub<TokenHub>(SignalRConstants.Token);
        });
    }
}