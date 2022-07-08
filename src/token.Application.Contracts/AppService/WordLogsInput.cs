using token.Application.Contracts.Module;
using token.Domain.Shared;

namespace token.Application.Contracts.AppService;

public class WordLogsInput : TokenInput
{
    public WordType? Type { get; set; }
    
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginDateTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndDateTime { get; set; }
}