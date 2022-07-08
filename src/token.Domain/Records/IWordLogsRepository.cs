using Volo.Abp.Domain.Repositories;

namespace token.Domain.Records;

public interface IWordLogsRepository : IRepository<WordLogs,Guid>
{
    
}