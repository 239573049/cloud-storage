using CloudStorage.Domain.Users.property;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CloudStorage.EntityFrameworkCore.Users.property;

public class UserStoragesRepository :EfCoreRepository<CloudStorageDbContext,UserStorages,Guid>,IUserStoragesRepository
{
    public UserStoragesRepository(IDbContextProvider<CloudStorageDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    
}