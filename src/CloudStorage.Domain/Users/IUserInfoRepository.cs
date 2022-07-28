using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Domain.Users;

/// <summary>
/// 用户仓储
/// </summary>
public interface IUserInfoRepository : IRepository<UserInfo>
{
    /// <summary>
    /// 获取用户基本信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserInfoView?> GetAsync(Guid id);
}