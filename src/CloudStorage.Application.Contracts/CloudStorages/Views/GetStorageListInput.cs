using token.Domain;

namespace CloudStorage.Application.Contracts.CloudStorages.Views;

/// <summary>
/// 获取云盘列表
/// </summary>
public class GetStorageListInput :PagedRequestDto
{
    /// <summary>
    /// 搜索
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// 上层文件夹id
    /// </summary>
    public Guid? StorageId { get; set; }
}