using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using token.Domain.Shared;
using token.SignalR.Web.Hubs;
using ILogger = Serilog.ILogger;

namespace token.SignalR.Web.Controllers;

/// <summary>
/// Token服务
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TokenController:ControllerBase
{
    private readonly IHubContext<TokenHub> _hubContext;
    private readonly ILogger _logger;
    /// <inheritdoc />
    public TokenController(IHubContext<TokenHub> hubContext, ILogger logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// 获取Token服务在线人数
    /// </summary>
    /// <returns></returns>
    [HttpGet("online-number")]
    public async Task<long> GetOnlineNumberAsync()
    {
        _logger.Debug("获取在线人数");
        var count=await RedisHelper.GetAsync<long>(SignalRConstants.TokenName);

        return count;
    }
}
