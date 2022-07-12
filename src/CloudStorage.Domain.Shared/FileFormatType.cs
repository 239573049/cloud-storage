namespace CloudStorage.Domain.Shared;

/// <summary>
/// 文件格式
/// </summary>
public enum FileFormatType
{
    /// <summary>
    /// 默认
    /// </summary>
    None,
    
    /// <summary>
    /// 图片
    /// </summary>
    Img,
    
    /// <summary>
    /// 视频
    /// </summary>
    Video,
    
    /// <summary>
    /// 文档
    /// </summary>
    Word,
    
    /// <summary>
    /// 文件夹
    /// </summary>
    Directory
}