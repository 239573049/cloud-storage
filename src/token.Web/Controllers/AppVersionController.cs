using Microsoft.AspNetCore.Mvc;
using token.Application.Contracts.AppService;
using token.Application.Contracts.Version;
using Volo.Abp;

namespace token.Controllers;

/// <summary>
/// 版本模块
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AppVersionController:ControllerBase
{
    private readonly IAppVersionService _appVersionService;

    /// <inheritdoc />
    public AppVersionController(IAppVersionService appVersionService)
    {
        _appVersionService = appVersionService;
    }

    /// <summary>
    /// 创建程序版本信息
    /// </summary>
    /// <param name="dto"></param>
    [HttpPost("app-version")]
    public async Task CreateAppVersionAsync(AppVersionDto dto)
    {
        // throw new BusinessException(message:"错误");
        await _appVersionService.CreateAppVersionAsync(dto);
    }
    
    /// <summary>
    /// 获取程序版本信息列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    [HttpGet("app-version-list")]
    public async Task<List<AppVersionDto>> GetAppVersionListAsync(string? keyword)
    {
        return await _appVersionService.GetAppVersionListAsync(keyword);
    }
    
    /// <summary>
    /// 通过编号获取产品版本信息
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("app-version/{code}")]
    public async Task<AppVersionDto> GetAppVersionAsync(string code)
    {
        return await _appVersionService.GetAppVersionAsync(code);
    }

    /// <summary>
    /// 编辑产品版本信息
    /// </summary>
    /// <param name="dto"></param>
    [HttpPut("app-version")]
    public async Task UpdateAppVersionAsync(AppVersionDto dto)
    {
        await _appVersionService.UpdateAppVersionAsync(dto);
    }

    /// <summary>
    /// 修改产品更新地址
    /// </summary>
    /// <param name="id"></param>
    /// <param name="download"></param>
    [HttpPut("update-download/{id}")]
    public async Task UpdateDownloadAsync(Guid id, string download)
    {
        await _appVersionService.UpdateDownloadAsync(id, download);
    }
}