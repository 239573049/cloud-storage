namespace token.Domain;

public interface IFacilityLoggerRepository
{
    /// <summary>
    /// 获取设备日志
    /// </summary>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<List<FacilityLogger>> GetFacilityLoggerListAsync(Guid facilityId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="facilityLogger"></param>
    /// <returns></returns>
    Task CreateFacilityLoggerAsync(FacilityLogger facilityLogger);
}