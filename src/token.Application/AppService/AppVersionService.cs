using token.Application.Contracts.AppService;
using token.Application.Contracts.Version;
using token.Domain;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace token.Application.AppService;

/// <summary>
/// 
/// </summary>
public class AppVersionService : ApplicationService, IAppVersionService
{
    private readonly IAppVersionRepository _appVersionRepository;

    /// <inheritdoc />
    public AppVersionService(IAppVersionRepository appVersionRepository)
    {
        _appVersionRepository = appVersionRepository;
    }


    /// <inheritdoc />
    public async Task CreateAppVersionAsync(AppVersionDto dto)
    {
        if (await _appVersionRepository.AnyAsync(x => x.Code == dto.Code))
        {
            throw new BusinessException(message: "编号已经存在");
        }

        var data = ObjectMapper.Map<AppVersionDto, AppVersion>(dto);
        data = await _appVersionRepository.InsertAsync(data);
    }

    /// <inheritdoc />
    public async Task<List<AppVersionDto>> GetAppVersionListAsync(string? keyword)
    {
        var result = await _appVersionRepository.GetListAsync(x =>
            keyword.IsNullOrEmpty() || x.Code.Contains(keyword) || x.Name.Contains(keyword));

        var dto = ObjectMapper.Map<List<AppVersion>, List<AppVersionDto>>(result);

        return dto;
    }

    /// <inheritdoc />
    public async Task<AppVersionDto> GetAppVersionAsync(string code)
    {
        var result = await _appVersionRepository.FirstOrDefaultAsync(x => x.Code == code);

        var dto = ObjectMapper.Map<AppVersion, AppVersionDto>(result);

        return dto;
    }

    /// <inheritdoc />
    public async Task UpdateAppVersionAsync(AppVersionDto dto)
    {
        var result = await _appVersionRepository.GetAsync(x => x.Id == dto.Id);
        if (result == null)
        {
            throw new BusinessException(message: "数据不存在");
        }

        ObjectMapper.Map(dto, result);
        await _appVersionRepository.UpdateAsync(result);
    }

    /// <inheritdoc />
    public async Task UpdateDownloadAsync(Guid id, string download)
    {
        var app = await _appVersionRepository.FirstOrDefaultAsync(x => x.Id == id);

        if (app == null)
        {
            throw new BusinessException(message: "不存在版本信息");
        }

        app.Download = download;
        app.UpdateTime=DateTime.Now;
        await _appVersionRepository.UpdateAsync(app);
    }
}