using Microsoft.AspNetCore.Mvc;
using token.Application.Contracts.AppService;

namespace token.SignalR.Web.Controllers;

/// <summary>
/// 设备
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FacilityController
{
    private readonly IFacilityService _facilityService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="facilityService"></param>
    public FacilityController(IFacilityService facilityService)
    {
        _facilityService = facilityService;
    }

    /// <summary>
    /// 获取设备日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("logger-list/{id:guid}")]
    public async Task<List<FacilityLoggerDto>> GetLoggerListAsync(Guid id)
    {
        return await _facilityService.GetFacilityLoggerListAsync(id);
    }
}