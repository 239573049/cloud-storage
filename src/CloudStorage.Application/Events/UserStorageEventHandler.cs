using CloudStorage.Domain.CloudStorages;
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
    private readonly IStorageRepository _storageRepository;

    /// <inheritdoc />
    public UserStorageEventHandler(IUserStoragesRepository userStoragesRepository, IStorageRepository storageRepository)
    {
        _userStoragesRepository = userStoragesRepository;
        _storageRepository = storageRepository;
    }

    /// <inheritdoc />
    public async Task HandleEventAsync(UserStorageEto eventData)
    {
        var length = await _storageRepository.GetUseLengthAsync(eventData.UserId);

        var userStorage = await _userStoragesRepository.FirstOrDefaultAsync(x => x.UserId == eventData.UserId);

        if (userStorage != null)
        {
            userStorage.UsedSize = length ?? 0;
            await _userStoragesRepository.UpdateAsync(userStorage);
        }
    }
}