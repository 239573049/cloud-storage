using CloudStorage.Application.Contracts.Helper;
using CloudStorage.Application.Contracts.Users;
using CloudStorage.Application.Contracts.Users.Views;
using CloudStorage.Application.Contracts.UserStorage;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Application.Users;

/// <inheritdoc />
public class UserInfoService : ApplicationService, IUserInfoService
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IPrincipalAccessor _principalAccessor;
    private readonly IUserStorageAppService _userStorageAppService;

    /// <inheritdoc />
    public UserInfoService(IUserInfoRepository userInfoRepository, IPrincipalAccessor principalAccessor,
        IUserStorageAppService userStorageAppService)
    {
        _userInfoRepository = userInfoRepository;
        _principalAccessor = principalAccessor;
        _userStorageAppService = userStorageAppService;
    }

    /// <inheritdoc />
    public async Task CreateUserInfoAsync(UserInfoDto dto)
    {
        if (dto.Password.IsNullOrWhiteSpace())
            throw new BusinessException(message: "密码不能为空");

        if (await _userInfoRepository.AnyAsync(x => x.Account == dto.Account))
            throw new BusinessException(message: "存在相同账号！");


        var data = ObjectMapper.Map<UserInfoDto, UserInfo>(dto);
        data.CloudStorageRoot = Guid.NewGuid().ToString("N");
        data.Status = UserStatus.Normal;
        data = await _userInfoRepository.InsertAsync(data);

        // 创建用户云盘
        await _userStorageAppService.CreateUserStorageAsync(data.Id);

        if (!Directory.Exists(data.CloudStorageRoot))
        {
            Directory.CreateDirectory(data.CloudStorageRoot);
        }
    }

    /// <inheritdoc />
    public async Task<string> CreateTokenAsync(CreateTokenInput input)
    {
        var user = await _userInfoRepository.FirstOrDefaultAsync(x =>
            x.Account == input.Account && x.Password == input.Password);

        if (user == null)
            throw new BusinessException(code: "400", message: "账号密码错误");

        var token = await _principalAccessor.CreateTokenAsync(user);

        return token;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> GetAsync()
    {
        var user = await _userInfoRepository.GetAsync(_principalAccessor.UserId());

        var dto = ObjectMapper.Map<UserInfoView, UserInfoDto>(user);

        return dto;
    }
}