using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;

namespace CloudStorage.Application.Helpers;

/// <summary>
/// 文件工具
/// </summary>
public class FileHelper : ISingletonDependency
{
    /// <summary>
    /// 保存文件到本地
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="path"></param>
    public async Task SaveFileAsync(Stream stream, string path)
    {
        var fileStream = File.Create(path);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
        stream.Close();
    }
}