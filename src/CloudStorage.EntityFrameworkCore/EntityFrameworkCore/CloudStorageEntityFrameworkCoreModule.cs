using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.Modularity;

namespace CloudStorage.EntityFrameworkCore;

[DependsOn(
    typeof(AbpEntityFrameworkCoreMySQLModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule)
)]
public class CloudStorageEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CloudStorageDbContext>(options => { options.AddDefaultRepositories(true); });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also CloudStorageMigrationsDbContextFactory for EF Core tooling. */
            options.UseMySQL();
        });
    }
}