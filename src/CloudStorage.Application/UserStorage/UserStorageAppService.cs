using CloudStorage.Application.Contracts.Helper;
using CloudStorage.Application.Contracts.UserStorage;
using CloudStorage.Domain.Users.property;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Application.UserStorage;

/// <inheritdoc />
public class UserStorageAppService : ApplicationService, IUserStorageAppService
{
    private readonly IUserStoragesRepository _userStorageRepository;
    private readonly IPrincipalAccessor _principalAccessor;

    /// <inheritdoc />
    public UserStorageAppService(IUserStoragesRepository userStorageRepository, IPrincipalAccessor principalAccessor)
    {
        _userStorageRepository = userStorageRepository;
        _principalAccessor = principalAccessor;
    }

    /// <inheritdoc />
    public async Task CreateUserStorageAsync(Guid userId)
    {
        if (await _userStorageRepository.AnyAsync(x => x.UserId == userId))
        {
            return;
        }

        var userStorage = new UserStorages(Guid.NewGuid())
        {
            UserId = userId
        };

        await _userStorageRepository.InsertAsync(userStorage);
    }

    /// <inheritdoc />
    public async Task<UserStoragesDto> GetUserStorageAsync()
    {
        var result =await _userStorageRepository.FirstOrDefaultAsync(x => x.UserId == _principalAccessor.UserId());

        return ObjectMapper.Map<UserStorages, UserStoragesDto>(result);
    }
}