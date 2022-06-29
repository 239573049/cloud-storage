namespace token.Application.Contracts.AppService;

public interface IFacilityService
{
    /// <summary>
    /// 保存设备日志
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task CreateFacilityLoggerAsync(FacilityLoggerDto dto);

    /// <summary>
    /// 获取设备日志
    /// </summary>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<List<FacilityLoggerDto>> GetFacilityLoggerListAsync(Guid facilityId);
}