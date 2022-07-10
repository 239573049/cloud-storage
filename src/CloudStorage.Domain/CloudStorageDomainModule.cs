using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;

namespace CloudStorage.Domain;

[DependsOn(
    typeof(AbpAuditLoggingDomainModule)
    )]
public class CloudStorageDomainModule:AbpModule
{
    
}