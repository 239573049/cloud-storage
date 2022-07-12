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
    /// <param name="fileName"></param>
    public async Task SaveFileAsync(Stream stream, string path, string fileName)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        var fileStream = File.Create(Path.Combine(path,fileName));
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
        stream.Close();
    }
}