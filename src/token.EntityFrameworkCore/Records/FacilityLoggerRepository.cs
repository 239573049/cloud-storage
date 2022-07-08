using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore.Records;

/// <summary>
/// 
/// </summary>
public class FacilityLoggerRepository: EfCoreRepository<TokenDbContext, token.Domain.Records.FacilityLogger, Guid>,token.Domain.Records.IFacilityLoggerRepository
{
    /// <inheritdoc />
    public FacilityLoggerRepository(IDbContextProvider<TokenDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<token.Domain.Records.FacilityLogger>> GetFacilityLoggerListAsync(Guid facilityId)
    {
        var dbContent = await GetDbContextAsync();
        return await dbContent.FacilityLogger.Where(x => x.FacilityId == facilityId).OrderByDescending(x=>x.CreationTime).ToListAsync();
    }

    /// <inheritdoc />
    public async  Task CreateFacilityLoggerAsync(token.Domain.Records.FacilityLogger facilityLogger)
    {
        var dbContext = await GetDbContextAsync();
        await dbContext.FacilityLogger.AddAsync(facilityLogger);
    }
}