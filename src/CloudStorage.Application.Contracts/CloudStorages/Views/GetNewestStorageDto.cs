namespace CloudStorage.Application.Contracts.CloudStorages.Views;

public class GetNewestStorageDto
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 显示内容
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 路由
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreationTime { get; set; }
}