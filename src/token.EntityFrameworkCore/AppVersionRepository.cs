using token.Domain;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore;

public class AppVersionRepository:EfCoreRepository<TokenDbContext,AppVersion,Guid>, IAppVersionRepository
{
    public AppVersionRepository(IDbContextProvider<TokenDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    
    
}