using Microsoft.Extensions.Logging;
using token.Application.Contracts.AppService;
using token.Domain;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace token.Application.AppService;

/// <summary>
/// 设备
/// </summary>
public class FacilityService:ApplicationService,IFacilityService
{
    private readonly IFacilityLoggerRepository _facilityLoggerRepository;
    /// <inheritdoc />
    public FacilityService(IFacilityLoggerRepository facilityLoggerRepository)
    {
        _facilityLoggerRepository = facilityLoggerRepository;
    }

    /// <inheritdoc />
    public async Task CreateFacilityLoggerAsync(FacilityLoggerDto dto)
    {
        var data = ObjectMapper.Map<FacilityLoggerDto, FacilityLogger>(dto);

        await _facilityLoggerRepository.CreateFacilityLoggerAsync(data);
        
    }

    /// <inheritdoc />
    public async Task<List<FacilityLoggerDto>> GetFacilityLoggerListAsync(Guid facilityId)
    {
        var data =await _facilityLoggerRepository.GetFacilityLoggerListAsync(facilityId);
        var dto = ObjectMapper.Map<List<FacilityLogger>, List<FacilityLoggerDto>>(data);

        return dto;
    }
}