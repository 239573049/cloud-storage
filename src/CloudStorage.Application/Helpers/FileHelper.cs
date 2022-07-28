using CloudStorage.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace CloudStorage.Application.Helpers;

/// <summary>
/// 文件工具
/// </summary>
public class FileHelper : ISingletonDependency
{
    private readonly ILogger<FileHelper> _fileHelper;

    public FileHelper(ILogger<FileHelper> fileHelper)
    {
        _fileHelper = fileHelper;
    }

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
    
    /// <summary>
    /// 保存文件到本地
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    public async Task SaveFileAsync(byte[]? bytes, string path, string fileName)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        
        var fileStream = File.Create(Path.Combine(path,fileName));
        await fileStream.WriteAsync(bytes);
        fileStream.Close();
        bytes=null;
        
        _fileHelper.LogWarning("上传路径{0}，上传文件名字{1}",path,fileName);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="name"></param>
    public async Task DeleteFileAsync(string name)
    {
        if (File.Exists(name))
        {
            try
            {
                File.Delete(name);
            }
            catch (Exception e)
            {
                _fileHelper.LogError("{0} : message: {1}",DateTime.Now.ToString(Constants.DefaultFullDateFormat),e);
            }
        }
    }

    /// <summary>
    /// 批量删除文件
    /// </summary>
    /// <param name="names"></param>
    public async Task DeleteFileListAsync(List<string> names)
    {
        foreach (var name in names)
        {
            await DeleteFileAsync(name);
        }
    }
}