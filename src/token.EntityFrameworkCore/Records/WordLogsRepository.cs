using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using token.Domain.Records;
using token.Domain.Shared;

namespace token.EntityFrameworkCore.Records;

public class WordLogsRepository : EfCoreRepository<TokenDbContext, token.Domain.Records.WordLogs, Guid>,
                                  token.Domain.Records.IWordLogsRepository
{
    public WordLogsRepository(IDbContextProvider<TokenDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<int> GetCountAsync(WordType? type, string? keywords, DateTime? beginDateTime = null,
                                         DateTime? endDateTime = null)
    {
        var query = await CreateQueryAsync(type, keywords, beginDateTime, endDateTime);

        return await query.CountAsync();
    }

    public async Task<List<WordLogs>> GetListAsync(WordType? type, string? keywords,
                                                   DateTime? beginDateTime = null,
                                                   DateTime? endDateTime = null,
                                                   int skipCount = 0,
                                                   int maxResultCount = int.MaxValue)
    {
        var query = await CreateQueryAsync(type, keywords, beginDateTime, endDateTime);

        return await query.PageBy(skipCount, maxResultCount).ToListAsync();
    }

    private async Task<IQueryable<WordLogs>> CreateQueryAsync(WordType? type, string keywords,
                                                              DateTime? beginDateTime = null,
                                                              DateTime? endDateTime = null)
    {
        var dbContext = await GetDbContextAsync();

        var query = dbContext.WordLogs
                             .WhereIf(type.HasValue, x => x.Type == type)
                             .WhereIf(!keywords.IsNullOrEmpty(),
                                      x => x.Device.Contains(keywords) || x.ip.Contains(keywords))
                             .WhereIf(beginDateTime.HasValue, x => x.CreationTime >= beginDateTime)
                             .WhereIf(endDateTime.HasValue, x => x.CreationTime <= endDateTime);

        return query;
    }
}