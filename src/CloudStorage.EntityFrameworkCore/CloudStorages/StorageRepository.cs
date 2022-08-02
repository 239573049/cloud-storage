using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;

namespace CloudStorage.EntityFrameworkCore.CloudStorages;

public class StorageRepository : EfCoreRepository<CloudStorageDbContext, Storage, Guid>, IStorageRepository
{
    private readonly IDistributedEventBus _distributedEventBus;
    public StorageRepository(IDbContextProvider<CloudStorageDbContext> dbContextProvider, IDistributedEventBus distributedEventBus) : base(dbContextProvider)
    {
        _distributedEventBus = distributedEventBus;
    }

    /// <inheritdoc />
    public async Task<Storage?> GetNewestFileAsync(Guid userId)
    {
        var dbContext = await GetDbContextAsync();

        var query = dbContext.Storage.Where(x => x.Type == StorageType.File && x.UserInfoId == userId)
            .OrderByDescending(x => x.CreationTime);

        return await query.FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<long?> GetUseLengthAsync(Guid userId)
    {
        var dbContext = await GetDbContextAsync();

        var query =
            dbContext.Storage.Where(x => x.UserInfoId == userId && x.Type == StorageType.File && x.Length != null)
                .SumAsync(x => x.Length);

        return await query;
    }

    /// <inheritdoc />
    public async Task CreateAsync(Storage storage)
    {
        var dbContext = await GetDbContextAsync();

        await dbContext.Storage.AddAsync(storage);
    }
}