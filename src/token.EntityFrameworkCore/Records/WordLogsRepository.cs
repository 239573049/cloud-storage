using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore.Records;

public class WordLogsRepository : EfCoreRepository<TokenDbContext,token.Domain.Records.WordLogs,Guid>,token.Domain.Records.IWordLogsRepository
{
    public WordLogsRepository(IDbContextProvider<TokenDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}