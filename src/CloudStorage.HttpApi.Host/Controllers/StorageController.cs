using CloudStorage.Application.Contracts.CloudStorages;
using CloudStorage.Application.Contracts.CloudStorages.Views;
using Microsoft.AspNetCore.Mvc;

namespace token.Controllers;

/// <summary>
/// 云盘
/// </summary>
[ApiController]
[Route("api/storage")]
public class StorageController : ControllerBase
{
    private readonly IStorageService _storageService;

    /// <inheritdoc />
    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="storageId"></param>
    /// <returns></returns>
    [HttpPost("upload-file")]
    public async Task<StorageDto> UploadFilesAsync(IFormFile file,[FromQuery]Guid? storageId = null)
    {
        var dto = await _storageService.UploadFilesAsync(file, storageId);

        return dto;
    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("storage-list")]
    public async Task<List<StorageDto>> GetStorageListAsync([FromQuery]GetStorageListInput input)
    {
        return await _storageService.GetStorageListAsync(input);
    }

    /// <summary>
    /// 获取最新的文件
    /// </summary>
    /// <returns></returns>
    [HttpGet("newest-file")]
    public async Task<StorageDto> GetNewestFile()
    {
        return await _storageService.GetNewestFile();
    }
}