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
}