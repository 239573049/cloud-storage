namespace CloudStorage.Domain.Shared.Events;

/// <summary>
/// 云盘使用Eto
/// </summary>
public class UserStorageEto 
{
    /// <summary>
    /// 长度
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    public UserStorageEto(long? length, Guid userId)
    {
        Length = length;
        UserId = userId;
    }
}