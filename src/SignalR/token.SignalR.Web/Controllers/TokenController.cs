using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using token.Domain.Shared;
using token.SignalR.Web.Hubs;

namespace token.SignalR.Web.Controllers;

/// <summary>
/// Token服务
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TokenController:ControllerBase
{
    private readonly IHubContext<TokenHub> _hubContext;

    public TokenController(IHubContext<TokenHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <summary>
    /// 获取Token服务在线人数
    /// </summary>
    /// <returns></returns>
    [HttpGet("online-number")]
    public async Task<long> GetOnlineNumberAsync()
    {
        var count=await RedisHelper.GetAsync<long>(SignalRConstants.TokenName);

        return count;
    }

    /// <summary>
    /// 发送信息至所有用户
    /// </summary>
    /// <param name="data"></param>
    [HttpPost]
    public async Task SendMessage([FromBody]string data)
    {
        await _hubContext.Clients.All.SendAsync(data);
    }
}
