namespace CloudStorage.Domain.Users;

public class UserInfoView : UserInfo
{
    /// <summary>
    /// 用户总大小
    /// </summary>
    public long TotalSize { get; set; }

    /// <summary>
    /// 已经使用大小
    /// </summary>
    public long UsedSize { get; set; } = 0;

    public UserInfoView(long totalSize, long usedSize)
    {
        TotalSize = totalSize;
        UsedSize = usedSize;
    }

    public UserInfoView(Guid id, long totalSize, long usedSize) : base(id)
    {
        TotalSize = totalSize;
        UsedSize = usedSize;
    }
}