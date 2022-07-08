using Volo.Abp.Application.Services;
using token.Application.Contracts.AppService;
using token.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace token.Application.AppService;

public class WordLogsService : ApplicationService, IWordLogsService
{
    private readonly token.Domain.Records.IWordLogsRepository _wordLogsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WordLogsService(token.Domain.Records.IWordLogsRepository wordLogsRepository, IHttpContextAccessor httpContextAccessor)
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
}