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
        if (FileNameSuffix.Img.Contains(name))
        {
           return RedisHelper.GetAsync<string>(nameof(FileFormatType.Img));
        }
        else if (FileNameSuffix.Video.Contains(nameof(FileFormatType.Video)))
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.Video));
        }
        else if (FileNameSuffix.Word.Contains(nameof(FileFormatType.Word)))
        {
            return RedisHelper.GetAsync<string>(nameof(FileFormatType.Word));
        }
        else
        {
            return  RedisHelper.GetAsync<string>(nameof(FileFormatType.None));
        }
    }

    public static async Task SetIconAsync()
    {
        await RedisHelper.SetAsync(nameof(FileFormatType.Img),"https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/img.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.Video),"https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/video.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.Word),"https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/word.png");
        await RedisHelper.SetAsync(nameof(FileFormatType.None),"https://token-cloud-storage.oss-cn-shenzhen.aliyuncs.com/icon/unknownfile.png");
    }
}