using System.Security.Claims;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.HttpApi;

public interface IPrincipalAccessor
{
    string Name { get; }

    Guid ID { get; }
    string GetUser(string token);

    bool? IsAuthenticated();

    IEnumerable<Claim>? GetClaimsIdentity();

    List<string>? GetClaimValueByType(string claimType);

    string GetToken();

    string GetTenantId();

    /// <summary>
    ///     获取账号信息
    /// </summary>
    /// <returns></returns>
    T GetUserInfo<T>();

    List<string> GetUserInfoFromToken(string claimType);

    /// <summary>
    ///     获取用户id （未授权异常401）
    /// </summary>
    /// <returns></returns>
    Guid UserId();

    Task<string> CreateTokenAsync<T>(T userInfo) where T : Entity<Guid>;
}