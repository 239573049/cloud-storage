using CloudStorage.Domain.Users;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CloudStorage.EntityFrameworkCore.Users;

public class UserInfoRepository : EfCoreRepository<CloudStorageDbContext, UserInfo, Guid>, IUserInfoRepository ,ITransientDependency
{
    public UserInfoRepository(IDbContextProvider<CloudStorageDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}