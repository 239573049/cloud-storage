namespace CloudStorage.Domain.Shared.Options;

public class FileStreamOptions
{
    public const string Name = nameof(FileStreamOptions);

    /// <summary>
    /// 下载数量
    /// </summary>
    public int DownloadNumber { get; set; } = 2;
    
    
}