using CloudStorage.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CloudStorage.DbMigrator;

/// <summary>
/// 
/// </summary>
[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CloudStorageEntityFrameworkCoreModule)
)]
public class TokenDbMigratorModule : AbpModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}