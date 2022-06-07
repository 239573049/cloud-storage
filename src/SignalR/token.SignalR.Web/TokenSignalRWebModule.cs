﻿using token.Domain.Shared;
using token.HttpApi;
using token.SignalR.Web.Hubs;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace token.SignalR.Web;

[DependsOn(
    typeof(TokenHttpApiModule)
    )]
public class TokenSignalRWebModule:AbpModule
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
