using System.Runtime.InteropServices;

namespace CloudStorage.Domain.Shared;

public static class CloudStorageExtension
{
    /// <summary>
    /// 获取云盘存放路径
    /// </summary>
    /// <returns></returns>
    public static string CloudStorageRoot()
    {
        var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        return Path.Combine(path, Constants.CloudStorageRoot);
    }
}