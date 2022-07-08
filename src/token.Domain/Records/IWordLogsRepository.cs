using Volo.Abp.Domain.Repositories;
using token.Domain.Shared;

namespace token.Domain.Records;

public interface IWordLogsRepository : IRepository<WordLogs, Guid>
{
    Task<int> GetCountAsync(WordType? type, string? keywords, DateTime? beginDateTime = null,
                            DateTime? endDateTime = null);

    Task<List<WordLogs>> GetListAsync(WordType? type, string? keywords, DateTime? beginDateTime = null,
                                      DateTime? endDateTime = null, int skipCount = 0,
                                      int maxResultCount = int.MaxValue);
}