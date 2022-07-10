namespace CloudStorage.Domain.Shared;

public class Constants
{
    public const string ClaimKey = "id";
    
    public const string User = "user";

    public const string Role = "Role";


    public const string TenantHeader = "X-TenantId";


    public const string JwtHeader = "Authorization";

    public const string JwtType = "Bearer ";

    public const string JsonType = "application/json";

    public const string CorsPolicy = nameof(CorsPolicy);
    
    public const string DefaultTodayDateFormat = "yyyy-MM-dd";

    public const string DefaultFullDateFormat = "yyyy-MM-dd HH:mm:ss";

    public const string DefaultTodayDateStr = "yyyyMMdd";

    public const string DefaultFullDateStr = "yyyyMMddHHmmss";

    /// <summary>
    /// 云盘存放路径
    /// </summary>
    public const string CloudStorageRoot = "./wwwroot/CloudStorage";

}