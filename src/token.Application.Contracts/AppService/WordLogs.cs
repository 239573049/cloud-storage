using Volo.Abp.Domain.Entities.Auditing;
using token.Domain.Shared;

namespace token.Application.Contracts.AppService;

public class WordLogsDto : CreationAuditedEntity<Guid>
{
    public WordType Type { get; set; }

    /// <summary>
    /// 请求ip
    /// </summary>
    public string ip { get; set; }

    /// <summary>
    /// 请求设备
    /// </summary>
    public string Device { get; set; }
}