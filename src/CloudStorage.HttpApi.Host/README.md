## 迁移文件

生成迁移文件
dotnet ef migrations add AddBlogCreatedTimestamp --project ../CloudStorage.EntityFrameworkCore

更新迁移文件
dotnet ef database update  --project ../CloudStorage.EntityFrameworkCore

删除上次迁移文件
dotnet ef migrations remove --project ../CloudStorage.EntityFrameworkCore