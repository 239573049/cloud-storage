using System.ComponentModel.DataAnnotations.Schema;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace CloudStorage.Domain.CloudStorages;

/// <summary>
/// 云盘管理
/// </summary>
public class Storage: AggregateRoot<Guid>, IHasCreationTime
{
    /// <summary>
    /// 类型
    /// </summary>
    public StorageType Type { get; set; }

    /// <summary>
    /// 云盘实际路径
    /// </summary>
    public string? StoragePath { get; set; }

    /// <summary>
    /// 虚拟路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 上级文件夹Id
    /// </summary>
    public Guid? StorageId { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long? Length { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public Guid UserInfoId { get; set; }

    /// <summary>
    /// 云盘下载路径
    /// </summary>
    [NotMapped]
    public string CloudUrl
    {
        get
        {
            return StoragePath?.Replace(Constants.CloudStorageWWWROOT, "").Replace("\\","/");
        }
    }

    public virtual UserInfo UserInfo { get; set; }
    
    public DateTime CreationTime { get; set; }

    public Storage()
    {
    }

    public Storage(Guid id) : base(id)
    {
    }
}