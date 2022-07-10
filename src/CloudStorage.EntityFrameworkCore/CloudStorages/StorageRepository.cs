using CloudStorage.Domain.CloudStorages;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CloudStorage.EntityFrameworkCore.CloudStorages;

public class StorageRepository :EfCoreRepository<CloudStorageDbContext,Storage,Guid>,IStorageRepository
{
    public StorageRepository(IDbContextProvider<CloudStorageDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}