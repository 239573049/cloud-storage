using CloudStorage.Domain.Shared.Events;
using CloudStorage.Domain.Users.property;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;

namespace CloudStorage.Application.Events;

/// <summary>
/// 用户上传处理
/// </summary>
public class UserStorageEventHandler
    : ILocalEventHandler<UserStorageEto>,
        ITransientDependency
{
    private readonly IUserStoragesRepository _userStoragesRepository;

    /// <summary>
    /// 用户上传处理
    /// </summary>
    /// <param name="userStoragesRepository"></param>
    public UserStorageEventHandler(IUserStoragesRepository userStoragesRepository)
    {
        _userStoragesRepository = userStoragesRepository;
    }

    /// <inheritdoc />
    public async Task HandleEventAsync(UserStorageEto eventData)
    {
        var userStorage = await _userStoragesRepository.FirstOrDefaultAsync(x => x.UserId == eventData.UserId);

        if (userStorage != null)
        {
            userStorage.UsedSize += eventData.Length ?? 0;
            await _userStoragesRepository.UpdateAsync(userStorage);
        }
    }
}