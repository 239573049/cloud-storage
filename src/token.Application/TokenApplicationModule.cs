using Microsoft.Extensions.DependencyInjection;
using token.Application.AutoMapper;
using token.Application.Contracts;
using token.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace token.Application;

[DependsOn(
    typeof(TokenEntityFrameworkCoreModule),
    typeof(TokenApplicationContractsModule),
    typeof(AbpAutoMapperModule)
)]
public class TokenApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TokenApplicationModule>();

        Configure<AbpAutoMapperOptions>(option =>
        {
            option.AddMaps<TokenApplicationModule>();
            option.AddProfile<TokenApplicationAutoMapperProfile>();
        });
    }
}