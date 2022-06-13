using token.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace token.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TokenEntityFrameworkCoreModule)
)]
public class TokenDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}