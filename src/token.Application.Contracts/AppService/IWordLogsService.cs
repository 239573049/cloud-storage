namespace token.Application.Contracts.AppService;
using token.Domain.Shared;
public interface IWordLogsService
{
    /// <summary>
    /// 增加使用记录
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Task CreateWordLogsAsync(WordType type);
}