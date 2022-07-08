namespace token.Domain.Records;

public interface IFacilityLoggerRepository
{
    /// <summary>
    /// 获取设备日志
    /// </summary>
    /// <param name="facilityId"></param>
    /// <returns></returns>
    Task<List<token.Domain.Records.FacilityLogger>> GetFacilityLoggerListAsync(Guid facilityId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="facilityLogger"></param>
    /// <returns></returns>
    Task CreateFacilityLoggerAsync(token.Domain.Records.FacilityLogger facilityLogger);
}