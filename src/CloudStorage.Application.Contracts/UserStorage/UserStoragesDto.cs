using CloudStorage.Domain.Shared;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.Application.Contracts.UserStorage;

public class UserStoragesDto : AggregateRoot<Guid>, IHasCreationTime
{
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户总大小
    /// </summary>
    public long TotalSize { get; set; } = DefaultConstants.Size;

    /// <summary>
    /// 已经使用大小
    /// </summary>
    public long UsedSize { get; set; } = 0;

    public DateTime CreationTime { get; }
}