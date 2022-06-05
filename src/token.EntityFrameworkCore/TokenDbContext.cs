using Microsoft.EntityFrameworkCore;
using token.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class TokenDbContext:AbpDbContext<TokenDbContext>
{
    public DbSet<AppVersion> AppVersion { get; set; }
    
    public TokenDbContext(DbContextOptions<TokenDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 只禁用查询跟踪
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

#if DEBUG

        //开启更加详细的日志，以性能为代价
        optionsBuilder.EnableDetailedErrors(true);

#endif
        
    }
    
}