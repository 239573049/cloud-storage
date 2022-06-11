using Microsoft.AspNetCore.Mvc;
using token.Application.Contracts.Version;

namespace token.Controllers;

/// <summary>
///     版本模块
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AppVersionController : ControllerBase
{
    private readonly IAppVersionAppService _appVersionAppService;

    /// <inheritdoc />
    public AppVersionController(IAppVersionAppService appVersionAppService)
    {
        _appVersionAppService = appVersionAppService;
    }

    /// <summary>
    ///     创建程序版本信息
    /// </summary>
    /// <param name="dto"></param>
    [HttpPost("app-version")]
    public async Task CreateAppVersionAsync(AppVersionDto dto)
    {
        await _appVersionAppService.CreateAppVersionAsync(dto);
    }

    /// <summary>
    ///     获取程序版本信息列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    [HttpGet("app-version-list/{keyword}")]
    public async Task<List<AppVersionDto>> GetAppVersionListAsync(string keyword)
    {
        return await _appVersionAppService.GetAppVersionListAsync(keyword);
    }

    /// <summary>
    ///     通过编号获取产品版本信息
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet("app-version/{code}")]
    public async Task<AppVersionDto> GetAppVersionAsync(string code)
    {
        return await _appVersionAppService.GetAppVersionAsync(code);
    }

    /// <summary>
    ///     编辑产品版本信息
    /// </summary>
    /// <param name="dto"></param>
    [HttpPut("app-version")]
    public async Task UpdateAppVersionAsync(AppVersionDto dto)
    {
        await _appVersionAppService.UpdateAppVersionAsync(dto);
    }
}