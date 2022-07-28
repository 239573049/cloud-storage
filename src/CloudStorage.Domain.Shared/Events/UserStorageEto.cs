namespace CloudStorage.Domain.Shared.Events;

/// <summary>
/// 云盘使用Eto
/// </summary>
public class UserStorageEto 
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    public UserStorageEto(Guid userId)
    {
        UserId = userId;
    }
}