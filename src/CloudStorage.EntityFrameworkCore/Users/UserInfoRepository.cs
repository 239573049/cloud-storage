using CloudStorage.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CloudStorage.EntityFrameworkCore.Users;

/// <inheritdoc />
public class UserInfoRepository : EfCoreRepository<CloudStorageDbContext, UserInfo, Guid>, IUserInfoRepository ,ITransientDependency
{
    /// <inheritdoc />
    public UserInfoRepository(IDbContextProvider<CloudStorageDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <inheritdoc />
    public async Task<UserInfoView?> GetAsync(Guid id)
    {
        var dbContext = await GetDbContextAsync();

        // TODO 获取用户的基本信息包括云盘剩余大小
        var query =
            from userInfo in dbContext.UserInfo
            join userStorage in dbContext.UserStorages on userInfo.Id equals userStorage.UserId
            where userInfo.Id == id
            select new UserInfoView(userInfo.Id,userStorage.TotalSize,userStorage.UsedSize)
            {
                Account = userInfo.Account,
                BriefIntroduction = userInfo.BriefIntroduction,
                HeadPortraits = userInfo.HeadPortraits,
                Name = userInfo.Name,
                Sex = userInfo.Sex,
                WeChatOpenId = userInfo.WeChatOpenId,
                Status =userInfo.Status,
                Password = userInfo.Password,
            };

        return await query.FirstOrDefaultAsync();
    }
}