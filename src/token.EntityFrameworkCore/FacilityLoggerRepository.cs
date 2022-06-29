using Microsoft.EntityFrameworkCore;
using token.Domain;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace token.EntityFrameworkCore;

/// <summary>
/// 
/// </summary>
public class FacilityLoggerRepository: EfCoreRepository<TokenDbContext, FacilityLogger, Guid>,IFacilityLoggerRepository
{
    /// <inheritdoc />
    public FacilityLoggerRepository(IDbContextProvider<TokenDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<FacilityLogger>> GetFacilityLoggerListAsync(Guid facilityId)
    {
        var dbContent = await GetDbContextAsync();
        return await dbContent.FacilityLogger.Where(x => x.FacilityId == facilityId).OrderByDescending(x=>x.CreationTime).ToListAsync();
    }

    /// <inheritdoc />
    public async  Task CreateFacilityLoggerAsync(FacilityLogger facilityLogger)
    {
        var dbContext = await GetDbContextAsync();
        await dbContext.FacilityLogger.AddAsync(facilityLogger);
    }
}