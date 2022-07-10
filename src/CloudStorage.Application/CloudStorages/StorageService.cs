using CloudStorage.Application.Contracts.CloudStorages;
using CloudStorage.Application.Contracts.CloudStorages.Views;
using CloudStorage.Application.Contracts.Helper;
using CloudStorage.Application.Helpers;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using Microsoft.AspNetCore.Http;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CloudStorage.Application.CloudStorages;

/// <summary>
/// 云盘
/// </summary>
public class StorageService : ApplicationService, IStorageService
{
    private readonly IStorageRepository _storageRepository;
    private readonly IPrincipalAccessor _principalAccessor;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly FileHelper _fileHelper;

    /// <inheritdoc />
    public StorageService(IStorageRepository storageRepository, IPrincipalAccessor principalAccessor,
        IUserInfoRepository userInfoRepository, FileHelper fileHelper)
    {
        _storageRepository = storageRepository;
        _principalAccessor = principalAccessor;
        _userInfoRepository = userInfoRepository;
        _fileHelper = fileHelper;
    }


    public async Task<StorageDto> UploadFilesAsync(IFormFile file, Guid? storageId = null)
    {
        var userId = _principalAccessor.UserId();

        var path = string.Empty;
        if (storageId != null)
        {
            var storage = await _storageRepository.FirstOrDefaultAsync(x => x.Id == storageId);
            path = storage.Path;
        }

        var user = await _userInfoRepository.FirstOrDefaultAsync(x => x.Id == userId);

        var data = new Storage()
        {
            Path = Path.Combine(path, file.FileName),
            StorageId = storageId,
            UserInfoId = userId,
            Length = file.Length,
            Type = StorageType.File,
            StoragePath = Path.Combine(user.CloudStorageRoot, Guid.NewGuid().ToString("N") + file.FileName)
        };

        data = await _storageRepository.InsertAsync(data, true);

        await _fileHelper.SaveFileAsync(file.OpenReadStream(), data.StoragePath);

        return ObjectMapper.Map<Storage, StorageDto>(data);
    }

    public async Task<List<StorageDto>> GetStorageListAsync(GetStorageListInput input)
    {
        var userId = _principalAccessor.UserId();

        var storages =await _storageRepository.GetListAsync(x =>
            x.UserInfoId == userId && x.StorageId == input.StorageId &&
            (string.IsNullOrWhiteSpace(input.Keywords) || x.Path.Contains(input.Keywords)));

        var dto = ObjectMapper.Map<List<Storage>, List<StorageDto>>(storages);

        return dto;
    }

    public async Task<StorageDto> GetNewestFile()
    {
        var userId = _principalAccessor.UserId();

        var storage =await _storageRepository.GetNewestFileAsync(userId);

        return ObjectMapper.Map<Storage, StorageDto>(storage);
    }
}