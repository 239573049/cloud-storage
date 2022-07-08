using AutoMapper;
using token.Application.Contracts.AppService;
using token.Application.Contracts.Version;
using token.Domain;

namespace token.Application.AutoMapper;

public class TokenApplicationAutoMapperProfile : Profile
{
    public TokenApplicationAutoMapperProfile()
    {
        CreateMap<AppVersion, AppVersionDto>()
            .ReverseMap();

        CreateMap<FacilityLoggerDto, token.Domain.Records.FacilityLogger>().ReverseMap();

        CreateMap<WordLogsDto, token.Domain.Records.WordLogs>().ReverseMap();
    }
}