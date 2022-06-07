namespace token.Domain.Shared;

public class Constants
{
    public const string ClaimKey = "id";

    public const string EntityKey = "Id";
    public const string User = "user";

    public const string Role = "Role";

    public const string RowNoCn = "序号";

    public const string TenantHeader = "X-TenantId";

    public const string ClaimName = "Name";

    public const string JwtHeader = "Authorization";

    public const string JwtType = "Bearer ";

    public const string Permission = nameof(Permission);

    public const string JsonType = "application/json";

    public const string ModelStateResult = nameof(ModelStateResult);

    public const string CorsPolicy = nameof(CorsPolicy);
    public const string DefaultTodayDateFormat = "yyyy-MM-dd";

    public const string DefaultFullDateFormat = "yyyy-MM-dd HH:mm:ss";

    public const string DefaultTodayDateStr = "yyyyMMdd";

    public const string DefaultFullDateStr = "yyyyMMddHHmmss";

}