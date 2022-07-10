using CloudStorage.Domain.Shared;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.Application.Contracts.Users.Views;

public class UserInfoDto : AggregateRoot<Guid>, IHasCreationTime
{
    
    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? BriefIntroduction { get; set; }

    /// <summary>
    /// 微信openid
    /// </summary>
    public string? WeChatOpenId { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? HeadPortraits  { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public SexType Sex { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// 服务器文件
    /// </summary>
    public string? CloudStorageRoot { get; set; }
    
    public DateTime CreationTime { get; }
}