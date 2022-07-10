using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Domain.CloudStorages;

public interface IStorageRepository : IRepository<Storage>
{
    Task<Storage?> GetNewestFileAsync(Guid userId);
}