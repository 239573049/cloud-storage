using token.Domain.Shared;
using System.Linq.Dynamic.Core;

namespace token.Application.Contracts.AppService;
public interface IWordLogsService
{
    /// <summary>
    /// 增加使用记录
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    Task CreateWordLogsAsync(WordType type);

    /// <summary>
    /// 获取使用记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<Volo.Abp.Application.Dtos.PagedResultDto<WordLogsDto>> GetWordLogsListAsync(WordLogsInput input);
}