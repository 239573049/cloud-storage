namespace token.Application.Contracts.AppService;

public interface IPdfService
{
    /// <summary>
    /// 合并pdf文件
    /// </summary>
    /// <param name="streams"></param>
    /// <returns></returns>
    Task<byte[]> MangePdfAsync(List<Stream> streams);
    
    /// <summary>
    /// 图片转换pdf
    /// </summary>
    /// <param name="streams"></param>
    /// <returns></returns>
    Task<byte[]> ImgToPdfAsync(List<Stream> streams);
}
