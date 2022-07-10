using CloudStorage.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CloudStorage.Application.Contracts;
using CloudStorage.Application.Contracts.Helper;
using CloudStorage.Application.Helpers;
using CloudStorage.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CloudStorage.Application;

[DependsOn(
    typeof(CloudStorageEntityFrameworkCoreModule),
    typeof(TokenApplicationContractsModule),
    typeof(AbpAutoMapperModule)
)]
public class CloudStorageApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CloudStorageApplicationModule>();

        context.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        context.Services.AddTransient<IPrincipalAccessor, PrincipalAccessor>();
        
        Configure<AbpAutoMapperOptions>(option =>
        {
            option.AddMaps<CloudStorageApplicationModule>();
            option.AddProfile<CloudStorageApplicationAutoMapperProfile>();
        });
    }
}