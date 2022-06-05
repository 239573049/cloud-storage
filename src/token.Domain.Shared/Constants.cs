namespace token.Domain.Shared;

public class Constants
{
    public static string ClaimKey => "id";

    public static string EntityKey => "Id";
    public static string User => "user";

    public static string Role => "Role";

    public static string RowNoCn => "序号";

    public static string TenantHeader => "X-TenantId";

    public static string ClaimName => "Name";

    public static string JwtHeader => "Authorization";

    public static string JwtType => "Bearer ";

    public static string Permission => nameof(Permission);

    public static string JsonType => "application/json";

    public static string ModelStateResult => nameof(ModelStateResult);

    public static string CorsPolicy => nameof(CorsPolicy);
    public static string DefaultTodayDateFormat => "yyyy-MM-dd";

    public static string DefaultFullDateFormat => "yyyy-MM-dd HH:mm:ss";

    public static string DefaultTodayDateStr => "yyyyMMdd";

    public static string DefaultFullDateStr => "yyyyMMddHHmmss";

}