using Volo.Abp.Application.Services;
using token.Application.Contracts.AppService;
using token.Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using token.Domain.Records;
using Volo.Abp.Application.Dtos;
    
namespace token.Application.AppService;

public class WordLogsService : ApplicationService, IWordLogsService
{
    private readonly IWordLogsRepository _wordLogsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WordLogsService(IWordLogsRepository wordLogsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _wordLogsRepository = wordLogsRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateWordLogsAsync(WordType type)
    {
        var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        var device = _httpContextAccessor.HttpContext.Request.Headers["sec-ch-ua-platform"].ToString();

        var data = new token.Domain.Records.WordLogs()
        {
            ip = ip,
            Device = device,
            Type = type
        };

        data = await _wordLogsRepository.InsertAsync(data);
    }

    public async Task<PagedResultDto<WordLogsDto>> GetWordLogsListAsync(WordLogsInput input)
    {
        var count = await _wordLogsRepository.GetCountAsync(input.Type, input.Keywords, input.BeginDateTime,
                                                            input.EndDateTime);

        var result = await _wordLogsRepository.GetListAsync(input.Type, input.Keywords, input.BeginDateTime,
                                                            input.EndDateTime, input.SkipCount, input.MaxResultCount);

        var dto = ObjectMapper.Map<List<WordLogs>, List<WordLogsDto>>(result);

        return new PagedResultDto<WordLogsDto>(count,dto);
    }
}