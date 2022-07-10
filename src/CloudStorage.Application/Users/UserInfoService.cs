using CloudStorage.Application.Contracts.Users;
using CloudStorage.Application.Contracts.Users.Views;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using CloudStorage.HttpApi;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Application.Users;

public class UserInfoService : ApplicationService, IUserInfoService
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IPrincipalAccessor _principalAccessor;

    public UserInfoService(IUserInfoRepository userInfoRepository, IPrincipalAccessor principalAccessor)
    {
        _userInfoRepository = userInfoRepository;
        _principalAccessor = principalAccessor;
    }

    public async Task CreateUserInfoAsync(UserInfoDto dto)
    {
        if (dto.Password.IsNullOrWhiteSpace())
            throw new BusinessException(message: "密码不能为空");
        
        if (await _userInfoRepository.AnyAsync(x => x.Account == dto.Account))
            throw new BusinessException(message:"存在相同账号！");
        
        
        var data = ObjectMapper.Map<UserInfoDto, UserInfo>(dto);
        data.CloudStorageRoot = Path.Combine(CloudStorageExtension.CloudStorageRoot(), Guid.NewGuid().ToString("N"));
        data.Status = UserStatus.Normal;
        data =await _userInfoRepository.InsertAsync(data);

        if (!Directory.Exists(data.CloudStorageRoot))
        {
            Directory.CreateDirectory(data.CloudStorageRoot);
        }
    }

    public async Task<string> CreateTokenAsync(CreateTokenInput input)
    {
        var user =await _userInfoRepository.FirstOrDefaultAsync(x =>
            x.Account == input.Account && x.Password == input.Password);

        if (user == null)
            throw new BusinessException(code: "400", message: "账号密码错误");

        var token = await _principalAccessor.CreateTokenAsync(user);

        return token;
    }
}