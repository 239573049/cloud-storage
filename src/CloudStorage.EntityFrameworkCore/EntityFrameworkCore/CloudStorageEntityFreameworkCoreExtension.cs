using Microsoft.EntityFrameworkCore;
using CloudStorage.Domain;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using CloudStorage.Domain.Users.property;

namespace CloudStorage.EntityFrameworkCore;

/// <summary>
/// 初始云盘方法
/// </summary>
public static class CloudStorageEntityFreameworkCoreExtension
{
    /// <summary>
    /// 初始化云盘表结构
    /// </summary>
    /// <param name="builder"></param>
    public static void ConfigureStorage(this ModelBuilder builder)
    {
        builder.Entity<UserInfo>(x =>
        {
            x.HasComment("用户信息");
            x.ToTable(nameof(UserInfo));
            
            x.HasIndex(x => x.Id);
            x.HasKey(x => x.Id);

        });

        builder.Entity<Storage>(x =>
        {
            x.HasComment("云盘列表");
            x.ToTable(nameof(Storage));
            
            x.HasIndex(x => x.Id);
            x.HasKey(x => x.Id);

            x.HasIndex(x => x.UserInfoId);
            x.HasIndex(x => x.StorageId);
            x.HasIndex(x => x.Type);
        });


        builder.Entity<UserStorages>(x =>
        {
            x.HasComment("用户云盘可用大小");
            x.ToTable(nameof(UserStorages));

            x.HasIndex(x => x.Id);
            x.HasKey(x => x.Id);
            x.HasIndex(x => x.UserId);

        });
        
        builder.ConfigureCloudStorageInitData();
    }

    public static void ConfigureCloudStorageInitData(this ModelBuilder builder)
    {
        var userInfo = new UserInfo(Guid.NewGuid())
        {
            Account = "admin",
            Name = "admin",
            Password = "admin",
            Status = UserStatus.Normal,
            CloudStorageRoot = Guid.NewGuid().ToString("N"),
            Sex = SexType.None
        };
        
        builder.Entity<UserInfo>().HasData(userInfo);

        var userStorage = new UserStorages(Guid.NewGuid())
        {
            UserId = userInfo.Id
        };

        builder.Entity<UserStorages>().HasData(userStorage);
    }
}