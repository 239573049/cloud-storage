using CloudStorage.Application.Contracts.CloudStorages.Views;
using Microsoft.AspNetCore.Http;

namespace CloudStorage.Application.Contracts.CloudStorages;

public interface IStorageService
{
    /// <summary>
    /// 新增文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="storageId"></param>
    /// <returns></returns>
    Task<StorageDto> UploadFilesAsync(IFormFile file, Guid? storageId = null);

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<List<StorageDto>> GetStorageListAsync(GetStorageListInput input);

    /// <summary>
    /// 获取最新的文件
    /// </summary>
    /// <returns></returns>
    Task<GetNewestStorageDto> GetNewestFile();
}