using CloudStorage.Domain.Shared;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.Domain.Users.property;

/// <summary>
/// 云盘使用记录
/// </summary>
public class UserStorages : AggregateRoot<Guid>, IHasCreationTime
{
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户总大小
    /// </summary>
    public long TotalSize { get; set; }

    /// <summary>
    /// 已经使用大小
    /// </summary>
    public long UsedSize { get; set; } = 0;

    public DateTime CreationTime { get; }

    public UserStorages()
    {
        TotalSize = DefaultConstants.Size;
    }

    public UserStorages(Guid id):base(id)
    {
        TotalSize = DefaultConstants.Size;
    }
}