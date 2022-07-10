namespace CloudStorage.Application.Contracts.Module;

public class TokenInput : token.Domain.PagedRequestDto
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string? Keywords { get; set; }
}