namespace token.Application.Contracts.Version;

public interface IAppVersionAppService
{
    /// <summary>
    ///     创建产品版本信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task CreateAppVersionAsync(AppVersionDto dto);

    /// <summary>
    ///     获取产品列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<List<AppVersionDto>> GetAppVersionListAsync(string keyword);

    /// <summary>
    ///     通过编号获取版本信息
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<AppVersionDto> GetAppVersionAsync(string code);

    /// <summary>
    ///     编辑版本信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task UpdateAppVersionAsync(AppVersionDto dto);
}