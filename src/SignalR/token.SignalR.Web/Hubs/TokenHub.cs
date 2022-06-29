using Microsoft.AspNetCore.SignalR;
using Serilog;
using token.Application.Contracts.AppService;
using token.Domain.Shared;
using ILogger = Serilog.ILogger;

namespace token.SignalR.Web.Hubs;

/// <summary>
/// Token服务
/// </summary>
public class TokenHub:Hub
{
    private readonly IFacilityService _facilityService;
    private readonly ILogger _log;
    public TokenHub(IFacilityService facilityService,ILogger log)
    {
        _facilityService = facilityService;
        _log = log;
    }

    /// <summary>
    /// 连接处理
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        // 增加连接redis在线人数加一
        await RedisHelper.IncrByAsync(SignalRConstants.TokenName, 1);
        _log.Debug("链接服务:"+Context.ConnectionId);
    }

    /// <summary>
    /// 断开连接处理
    /// </summary>
    /// <param name="exception"></param>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // 断开连接redis在线人数减一
        await RedisHelper.IncrByAsync(SignalRConstants.TokenName, -1);
        _log.Debug("断开服务："+Context.ConnectionId);

    }

    /// <summary>
    /// 保存日志
    /// </summary>
    /// <param name="degreesCelsius"></param>
    /// <param name="percent"></param>
    public async Task LoggerAsync(string? degreesCelsius,string? percent)
    {
        var facilityId = Context.GetHttpContext()?.Request.Headers["FacilityId"].ToString();
        
        _log.Debug("facilityId:{FacilityId}; degreesCelsius:{DegreesCelsius} percent:{Percent}", facilityId, degreesCelsius, percent);
        await _facilityService.CreateFacilityLoggerAsync(new FacilityLoggerDto()
        {
            FacilityId = Guid.Parse(facilityId),
            Percent= percent,
            DegreesCelsius =degreesCelsius 
        });
    }
}
