using CloudStorage.Application.Contracts.CloudStorages;
using CloudStorage.Application.Contracts.CloudStorages.Views;
using CloudStorage.Application.Contracts.Helper;
using CloudStorage.Application.Helpers;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
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
    private readonly NameSuffix _nameSuffix;

    /// <inheritdoc />
    public StorageService(IStorageRepository storageRepository, IPrincipalAccessor principalAccessor,
        IUserInfoRepository userInfoRepository, FileHelper fileHelper, NameSuffix nameSuffix)
    {
        _storageRepository = storageRepository;
        _principalAccessor = principalAccessor;
        _userInfoRepository = userInfoRepository;
        _fileHelper = fileHelper;
        _nameSuffix = nameSuffix;
    }


    public async Task<StorageDto> UploadFilesAsync(UploadFileInput input, Guid? storageId = null)
    {
        var userId = _principalAccessor.UserId();

        if (storageId != null)
        {
            var storage = await _storageRepository.FirstOrDefaultAsync(x => x.Id == storageId);
            if (storage == null)
            {
                throw new BusinessException(message: "不存在上级文件夹");
            }
        }

        var user = await _userInfoRepository.FirstOrDefaultAsync(x => x.Id == userId);

        var fileName = Guid.NewGuid().ToString("N") + input.Name;
        var path = user.CloudStorageRoot;
        var data = new Storage()
        {
            Path = input.Name,
            StorageId = storageId,
            UserInfoId = userId,
            Length = input.Length,
            Type = StorageType.File,
            StoragePath = Path.Combine(path, fileName)
        };

        data = await _storageRepository.InsertAsync(data, true);

        await _fileHelper.SaveFileAsync(input.Bytes, path, fileName);

        return ObjectMapper.Map<Storage, StorageDto>(data);
    }

    public async Task UploadFileListAsync(List<UploadFileInput> files, Guid? storageId = null)
    {
        var userId = _principalAccessor.UserId();

        if (storageId != null)
        {
            var storage = await _storageRepository.FirstOrDefaultAsync(x => x.Id == storageId);
            if (storage == null)
            {
                throw new BusinessException(message: "不存在上级文件夹");
            }
        }

        var user = await _userInfoRepository.FirstOrDefaultAsync(x => x.Id == userId);

        foreach (var file in files)
        {
            var fileName = Guid.NewGuid().ToString("N") + file.Name;
            var path = user.CloudStorageRoot;
            var data = new Storage()
            {
                Path = file.Name,
                StorageId = storageId,
                UserInfoId = userId,
                Length = file.Length,
                Type = StorageType.File,
                StoragePath = Path.Combine(path, fileName)
            };

            data = await _storageRepository.InsertAsync(data, true);

            await _fileHelper.SaveFileAsync(file.Bytes, path, fileName);
        }
    }

    public async Task CreateDirectoryAsync(CreateDirectoryInput input)
    {
        var user = _principalAccessor.UserId();

        var data = new Storage()
        {
            Type = StorageType.Directory,
            Path = input.Path,
            StorageId = input.StorageId,
            UserInfoId = user,
        };

        data = await _storageRepository.InsertAsync(data);
    }

    public async Task<PagedResultDto<StorageDto>> GetStorageListAsync(GetStorageListInput input)
    {
        var userId = _principalAccessor.UserId();

        var storages = await _storageRepository.GetListAsync(x =>
            x.UserInfoId == userId && x.StorageId == input.StorageId &&
            (string.IsNullOrWhiteSpace(input.Keywords) || x.Path.Contains(input.Keywords)));

        var count = await _storageRepository.CountAsync(x =>
            x.UserInfoId == userId && x.StorageId == input.StorageId &&
            (string.IsNullOrWhiteSpace(input.Keywords) || x.Path.Contains(input.Keywords)));

        var dto = ObjectMapper.Map<List<Storage>, List<StorageDto>>(storages);

        foreach (var s in dto)
        {
            s.SetCloudUlr(await _nameSuffix.GetDefaultIconAsync(s?.CloudUrl));
            if (s.Type == StorageType.File)
            {
                s.Icon = await _nameSuffix.GetIconAsync(s.Path);
            }
            else
            {
                s.Icon = await _nameSuffix.GetDirectoryIconAsync();
            }
        }

        return new PagedResultDto<StorageDto>(count, dto);
    }

    /// <inheritdoc />
    public async Task<GetNewestStorageDto> GetNewestFile()
    {
        var userId = _principalAccessor.UserId();

        var storage = await _storageRepository.GetNewestFileAsync(userId);

        if (storage == null)
        {
            var newestStorage = new GetNewestStorageDto()
            {
                Id = Guid.Empty,
                FileName = string.Empty,
                Message = "最近未上传过文件哦",
                CreationTime = DateTime.Now,
                Title = "最近文件",
                Icon = string.Empty
            };
            return newestStorage;
        }
        else
        {
            var newestStorage = new GetNewestStorageDto()
            {
                Id = storage.Id,
                FileName = storage.Path,
                Message = "最近的上传的文件",
                CreationTime = storage.CreationTime,
                Title = "最近文件",
                Icon = await _nameSuffix.GetIconAsync(storage.Path)
            };
            return newestStorage;
        }
    }

    /// <inheritdoc />
    public async Task<StorageDto> GetStorageAsync(Guid id)
    {
        var data = await _storageRepository.FirstOrDefaultAsync(x => x.Id == id);

        var dto = ObjectMapper.Map<Storage, StorageDto>(data);

        dto.SetCloudUlr(await _nameSuffix.GetDefaultIconAsync(dto.CloudUrl));
        
        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid?> GoBackAsync(Guid? id)
    {
        var result =await _storageRepository.FirstOrDefaultAsync(x => x.Id == id);

        return result?.StorageId;
    }
}