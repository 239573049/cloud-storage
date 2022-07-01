using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace token.Domain;

/// <summary>
/// 用户表
/// </summary>
public class Users : CreationAuditedEntity<Guid>, ISoftDelete
{
    /// <summary>
    /// 账号
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    ///  密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///     手机号码
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    ///     昵称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     头像
    /// </summary>
    public string? HeadPortrait { get; set; }

    public bool IsDeleted { get; set; }
}