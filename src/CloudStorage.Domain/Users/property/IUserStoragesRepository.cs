using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Domain.Users.property;

/// <summary>
/// 用户云盘
/// </summary>
public interface IUserStoragesRepository : IRepository<UserStorages,Guid>
{
    
}