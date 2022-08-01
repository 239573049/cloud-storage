using System.Security.Principal;
using System.Threading.Channels;
using CloudStorage.Application.Contracts.Users.Views;
using CloudStorage.Application.Helpers;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Shared.Events;
using CloudStorage.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using token.Hubs.Views;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using FileStreamOptions = CloudStorage.Domain.Shared.Options.FileStreamOptions;

namespace token.Hubs;

/// <summary>
/// 文件上传服务Hub
/// </summary>
public class FileStreamHub : Hub
{
    private readonly IStorageRepository _storageRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IDistributedEventBus _distributedEventBus;
    private readonly ILogger<FileStreamHub> _logger;
    private readonly FileHelper _fileHelper;
    private readonly FileStreamOptions _fileStreamOptions;
    /// <inheritdoc />
    public FileStreamHub(IStorageRepository storageRepository, IUserInfoRepository userInfoRepository,
        FileHelper fileHelper, ILogger<FileStreamHub> logger, IDistributedEventBus distributedEventBus, IOptions<FileStreamOptions> fileStreamOptions)
    {
        _storageRepository = storageRepository;
        _userInfoRepository = userInfoRepository;
        _fileHelper = fileHelper;
        _logger = logger;
        _distributedEventBus = distributedEventBus;
        _fileStreamOptions = fileStreamOptions.Value;
    }

    public override async Task OnConnectedAsync()
    {
        await RedisHelper.SetAsync(GetUserId(), Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await RedisHelper.DelAsync(GetUserId());
    }

    /// <summary>
    /// 文件存储
    /// </summary>
    /// <returns></returns>
    public async Task FileStreamSaveAsync(ChannelReader<byte[]> stream, string json)
    {
        var file = JsonConvert.DeserializeObject<FileStreamView>(json);
        var userId = Guid.Parse(GetUserId());

        var user = await _userInfoRepository.FirstOrDefaultAsync(x => x.Id == userId);

        var number =await RedisHelper.GetAsync<long>(user.ToString());
        
        // TODO 限制用户下载线程
        if (number > _fileStreamOptions.DownloadNumber)
        {
            _logger.LogError("上传文件异常，上传用户ID：{0},上传数量达到上线；", userId);
            return;
        }
        
        await RedisHelper.IncrByAsync(user.ToString(), 1);

        var fileName = Guid.NewGuid().ToString("N") + file.FileName;

        var path = user?.CloudStorageRoot;

        var fileStream = await _fileHelper.CreateFileStreamAsync(path, fileName);

        var data = new Storage(Guid.NewGuid())
        {
            Path = fileName,
            StorageId = file.StorageId,
            UserInfoId = userId,
            Length = file.Length,
            Type = StorageType.File,
            StoragePath = Path.Combine(path, fileName)
        };

        await _storageRepository.InsertAsync(data, true);

        try
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var bytes))
                {
                    await fileStream.WriteAsync(bytes);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("上传文件异常，上传用户ID：{0}，异常：{1}", userId, e);
            await _fileHelper.DeleteFileAsync(path, fileName);
            await _storageRepository.DeleteAsync(data.Id);
        }
        finally
        {
            fileStream.Close();
            await RedisHelper.IncrByAsync(user.ToString(), -1);
        }

        // 发布上传文件事件处理
        await _distributedEventBus.PublishAsync(new UserStorageEto(userId));
    }

    private string GetUserId()
    {
        return Context.User?.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == Constants.Id)?.Value ??
               throw new Exception("未登录");
    }

    private UserInfo? GetUser()
    {
        var json = Context.User?.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == Constants.User)
                       ?.Value ??
                   throw new Exception("未登录");

        return JsonConvert.DeserializeObject<UserInfo>(json);
    }
}