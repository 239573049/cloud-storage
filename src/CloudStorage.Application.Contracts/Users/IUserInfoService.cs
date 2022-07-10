using CloudStorage.Application.Contracts.Users.Views;

namespace CloudStorage.Application.Contracts.Users;

public interface IUserInfoService
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task CreateUserInfoAsync(UserInfoDto dto);

    /// <summary>
    /// 获取Token
    /// </summary>
    /// <returns></returns>
    Task<string> CreateTokenAsync(CreateTokenInput input);
}