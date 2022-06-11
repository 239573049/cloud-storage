using token.Application;
using token.EntityFrameworkCore;
using token.HttpApi;
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
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}