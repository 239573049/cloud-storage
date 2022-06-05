using Volo.Abp.Domain.Repositories;

namespace token.Domain;

public interface IAppVersionRepository:IRepository<AppVersion,Guid>
{
    
}