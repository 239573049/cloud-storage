using Volo.Abp.Domain.Entities.Auditing;

namespace token.Domain.Records;

public class FacilityLogger: CreationAuditedEntity<Guid>
{
    /// <summary>
    /// 设备id
    /// </summary>
    public Guid FacilityId { get; set; }

    /// <summary>
    /// 温度
    /// </summary>
    public string? DegreesCelsius { get; set; }

    /// <summary>
    /// 湿度
    /// </summary>
    public string? Percent { get; set; }
}