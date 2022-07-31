using CloudStorage.Application.Contracts.CloudStorages;
using CloudStorage.Application.Contracts.CloudStorages.Views;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

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
    public async Task<StorageDto> UploadFilesAsync(IFormFile file,Guid? storageId = null)
    {
        var dto = await _storageService.UploadFilesAsync(new UploadFileInput()
        {
            Length = file.Length,
            Name = file.FileName,
            Bytes =await file.OpenReadStream().GetAllBytesAsync()
        }, storageId);

        return dto;
    }

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files"></param>
    /// <param name="storageId"></param>
    [HttpPost("upload-file-list")]
    public async Task UploadFileListAsync(List<IFormFile> files, Guid? storageId = null)
    {
        var data = files.Select( x => new UploadFileInput
        {
            Length = x.Length,
            Bytes = x.OpenReadStream().GetAllBytes(),
            Name = x.FileName
        }).ToList();
        
        await _storageService.UploadFileListAsync(data, storageId);
    }
    
    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("storage-list")]
    public async Task<PagedResultDto<StorageDto>> GetStorageListAsync([FromQuery]GetStorageListInput input)
    {
        return await _storageService.GetStorageListAsync(input);
    }

    /// <summary>
    /// 获取最新的文件
    /// </summary>
    /// <returns></returns>
    [HttpGet("newest-file")]
    public async Task<GetNewestStorageDto> GetNewestFile()
    {
        return await _storageService.GetNewestFile();
    }

    /// <summary>
    /// 新建文件夹
    /// </summary>
    /// <param name="input"></param>
    [HttpPost("directory")]
    public async Task CreateDirectoryAsync(CreateDirectoryInput input)
    {
        await _storageService.CreateDirectoryAsync(input);
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("storage/{id:guid}")]
    public async Task<StorageDto> GetStorageAsync(Guid id)
    {
        return await _storageService.GetStorageAsync(id);
    }

    /// <summary>
    /// 获取上一级id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("go-back")]
    public async Task<Guid?> GoBackAsync(Guid? id)
    {
        return await _storageService.GoBackAsync(id);
    }

    /// <summary>
    /// 删除指定文件或文件夹
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("storage/{id:guid}")]
    public async Task DeleteStorageAsync(Guid id)
    {
        await _storageService.DeleteStorageAsync(id);
    }
}