using CloudStorage.Domain.Shared;
using Volo.Abp.DependencyInjection;

namespace CloudStorage.Application.Helpers;

public class NameSuffix : ISingletonDependency
{
    /// <summary>
    /// 获取文件名称Icon
    /// </summary>
    /// <param name="name">文件名字</param>
    /// <returns></returns>
    public Task<string> GetIconAsync(string name)
    {
        if (FileNameSuffix.Img.Any(x=>name.EndsWith(x)))
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.Img));
        }
        else if (FileNameSuffix.Video.Any(x=>name.EndsWith(x)))
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.Video));
        }
        else if (FileNameSuffix.Word.Any(x=>name.EndsWith(x)))
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.Word));
        }
        else
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.None));
        }
    }

    /// <summary>
    /// 获取文件夹图标
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetDirectoryIconAsync()
    {
        return await RedisHelper.GetAsync<string>(nameof(FileFormatType.Directory));
    }

    public static async Task SetIconAsync()
    {
        await RedisHelper.SetAsync(nameof(FileFormatType.Img),
            "https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/img.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.Video),
            "https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/video.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.Word),
            "https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/word.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.None),
            "https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/unknownfile.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.Directory),
            "https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/directory.png");
    }
}