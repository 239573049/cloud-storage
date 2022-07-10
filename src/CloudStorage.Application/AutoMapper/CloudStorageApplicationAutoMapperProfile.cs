using AutoMapper;
using CloudStorage.Application.Contracts.CloudStorages.Views;
using CloudStorage.Application.Contracts.Users.Views;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Users;

namespace CloudStorage.Application.AutoMapper;

/// <summary>
/// 
/// </summary>
public class CloudStorageApplicationAutoMapperProfile : Profile
{
    /// <inheritdoc />
    public CloudStorageApplicationAutoMapperProfile()
    {
        ConfigureUserInfo();
        ConfigureStorage();
    }

    private void ConfigureUserInfo()
    {
        CreateMap<UserInfoDto,UserInfo>();
        CreateMap<UserInfo,UserInfoDto>();
    }
    
    private void ConfigureStorage()
    {
        CreateMap<StorageDto, Storage>();
        CreateMap<Storage, StorageDto>();
    }
}