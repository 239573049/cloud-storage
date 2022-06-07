using Microsoft.AspNetCore.Mvc;
using token.Domain.Shared;

namespace token.SignalR.Web.Controllers;

/// <summary>
/// Token服务
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TokenController:ControllerBase
{
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
}
