namespace CloudStorage.Application.Contracts.UserStorage;

/// <summary>
/// 用户云盘使用
/// </summary>
public interface IUserStorageAppService
{
    /// <summary>
    /// 创建用户云盘使用记录
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task CreateUserStorageAsync(Guid userId);

    /// <summary>
    /// 获取用户云盘信息
    /// </summary>
    /// <returns></returns>
    Task<UserStoragesDto> GetUserStorageAsync();
}