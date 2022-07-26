using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Users;
using CloudStorage.Domain.Users.property;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CloudStorage.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class CloudStorageDbContext : AbpDbContext<CloudStorageDbContext>
{
    public CloudStorageDbContext(DbContextOptions<CloudStorageDbContext> options) : base(options)
    {
    }

    public DbSet<UserInfo> UserInfo { get; set; }

    public DbSet<Storage?> Storage { get; set; }

    public DbSet<UserStorages> UserStorages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 只禁用查询跟踪
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

#if DEBUG

        //开启更加详细的日志，以性能为代价
        optionsBuilder.EnableDetailedErrors();

#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ExtraPropertyDictionary>();
        modelBuilder.ConfigureStorage();
        modelBuilder.ConfigureAuditLogging();
    }
}