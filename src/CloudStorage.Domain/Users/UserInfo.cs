using CloudStorage.Domain.Shared;
using Microsoft.Extensions.Primitives;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.Domain.Users;

/// <summary>
/// 用户聚合
/// </summary>
public class UserInfo : AggregateRoot<Guid>, ISoftDelete, IHasCreationTime
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
    public string? HeadPortraits { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public SexType Sex { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public UserStatus Status { get; set; }

    private string? _cloudStorageRoot;

    /// <summary>
    /// 服务器文件
    /// </summary>
    public string? CloudStorageRoot
    {
        get { return _cloudStorageRoot; }
        set { _cloudStorageRoot = Constants.CloudStorageRoot + "/" + value; }
    }

    public bool IsDeleted { get; set; }

    public DateTime CreationTime { get; }

    public UserInfo()
    {
    }

    public UserInfo(Guid id) : base(id)
    {
    }
}