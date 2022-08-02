using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Domain.CloudStorages;

public interface IStorageRepository : IRepository<Storage,Guid>
{
    /// <summary>
    /// 获取用户最新操作的
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<Storage?> GetNewestFileAsync(Guid userId);

    /// <summary>
    /// 获取用户使用大小
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<long?> GetUseLengthAsync(Guid userId);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="storage"></param>
    /// <returns></returns>
    Task CreateAsync(Storage storage);
}