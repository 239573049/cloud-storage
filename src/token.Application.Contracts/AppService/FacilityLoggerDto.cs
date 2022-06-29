namespace token.Application.Contracts.AppService;

/// <summary>
/// 
/// </summary>
public class FacilityLoggerDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
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