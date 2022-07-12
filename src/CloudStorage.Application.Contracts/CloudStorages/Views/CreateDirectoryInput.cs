namespace CloudStorage.Application.Contracts.CloudStorages.Views;

public class CreateDirectoryInput
{
    /// <summary>
    /// 上层文件夹id
    /// </summary>
    public Guid? StorageId { get; set; }

    /// <summary>
    /// 文件夹名称
    /// </summary>
    public string? Path { get; set; }
}