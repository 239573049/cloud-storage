using Microsoft.EntityFrameworkCore;
using CloudStorage.Domain;
using CloudStorage.Domain.CloudStorages;
using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;

namespace CloudStorage.EntityFrameworkCore;

public static class CloudStorageEntityFreameworkCoreExtension
{
    public static void ConfigureCloudStorage(this ModelBuilder builder)
    {
        builder.Entity<UserInfo>(x =>
        {
            x.ToTable(nameof(UserInfo));
            
            x.HasIndex(x => x.Id);
            x.HasKey(x => x.Id);

            x.HasComment("用户");
            
        });

        builder.Entity<Storage>(x =>
        {
            x.ToTable(nameof(Storage));
            
            x.HasIndex(x => x.Id);
            x.HasKey(x => x.Id);

            x.HasIndex(x => x.UserInfoId);
            x.HasIndex(x => x.StorageId);
            x.HasIndex(x => x.Type);


        });
    }

    public static void ConfigureCloudStorageInitData(this ModelBuilder builder)
    {
        var userInfo = new UserInfo(Guid.NewGuid())
        {
            Account = "admin",
            Name = "admin",
            Password = "admin",
            Status = UserStatus.Normal,
            CloudStorageRoot =Path.Combine( CloudStorageExtension.CloudStorageRoot(),"admin"),
            Sex = SexType.None
        };
        
        builder.Entity<UserInfo>().HasData(userInfo);
        
        
    }
}