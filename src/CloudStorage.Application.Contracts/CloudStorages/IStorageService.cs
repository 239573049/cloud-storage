using CloudStorage.Application.Contracts.CloudStorages.Views;
using CloudStorage.Domain.CloudStorages;
using Volo.Abp.Application.Dtos;

namespace CloudStorage.Application.Contracts.CloudStorages;

/// <summary>
/// 
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// 新增文件
    /// </summary>
    /// <param name="input"></param>
    /// <param name="storageId"></param>
    /// <returns></returns>
    Task<StorageDto> UploadFilesAsync(UploadFileInput input, Guid? storageId = null);

    /// <summary>
    /// 批量新增文件
    /// </summary>
    /// <param name="files"></param>
    /// <param name="storageId"></param>
    /// <returns></returns>
    Task UploadFileListAsync(List<UploadFileInput> files, Guid? storageId = null);
    
    /// <summary>
    /// 新建文件夹
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateDirectoryAsync(CreateDirectoryInput input);
    
    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<StorageDto>> GetStorageListAsync(GetStorageListInput input);

    /// <summary>
    /// 获取最新的文件
    /// </summary>
    /// <returns></returns>
    Task<GetNewestStorageDto> GetNewestFile();

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StorageDto> GetStorageAsync(Guid id);

    /// <summary>
    /// 获取上一层id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Guid?> GoBackAsync(Guid? id);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteStorageAsync(Guid id);

    /// <summary>
    /// 递归所有文件
    /// </summary>
    /// <param name="storages"></param>
    /// <param name="folderId">文件夹名称</param>
    /// <param name="ids">所有文件路径</param>
    /// <returns></returns>
    Task GetFolderListAsync(List<Storage> storages,Guid folderId, List<Guid> ids);
}