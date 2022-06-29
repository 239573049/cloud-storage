using Microsoft.EntityFrameworkCore;
using token.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class TokenDbContext : AbpDbContext<TokenDbContext>
{
    public TokenDbContext(DbContextOptions<TokenDbContext> options) : base(options)
    {
    }

    /// <summary>
    ///     版本
    /// </summary>
    public DbSet<AppVersion> AppVersion { get; set; }

    /// <summary>
    /// 设备
    /// </summary>
    public DbSet<FacilityLogger> FacilityLogger { get; set; }

    /// <summary>
    ///     用户
    /// </summary>
    public DbSet<Users> Users { get; set; }

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
        modelBuilder.AddConfig();
    }
}