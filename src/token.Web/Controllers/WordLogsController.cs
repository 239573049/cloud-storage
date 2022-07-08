using token.Application.Contracts.AppService;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace token.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordLogsController
{
    private readonly IWordLogsService _wordLogsService;

    public WordLogsController(IWordLogsService wordLogsService)
    {
        _wordLogsService = wordLogsService;
    }

    /// <summary>
    /// 操作日志
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("word-logs-list")]
    public async Task<PagedResultDto<WordLogsDto>> GetWordLogsListAsync([FromQuery] WordLogsInput input)
    {
        var result = await _wordLogsService.GetWordLogsListAsync(input);

        return result;
    }
}