using Microsoft.AspNetCore.SignalR;
using token.Domain.Shared;

namespace token.SignalR.Web.Hubs;

/// <summary>
/// Token服务
/// </summary>
public class TokenHub:Hub
{
    /// <summary>
    /// 连接处理
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        // 增加连接redis在线人数加一
        await RedisHelper.IncrByAsync(SignalRConstants.TokenName, 1);
    }

    /// <summary>
    /// 断开连接处理
    /// </summary>
    /// <param name="exception"></param>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // 断开连接redis在线人数减一
        await RedisHelper.IncrByAsync(SignalRConstants.TokenName, -1);
    }

    /// <summary>
    /// 发送至全部在线人员
    /// </summary>
    /// <param name="value"></param>
    // [HubMethodName("send-all")]
    public async Task SendAllAsync(string value)
    {
        await Clients.All.SendAsync("all-message", value);
    }
}
