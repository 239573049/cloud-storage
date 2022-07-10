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
}