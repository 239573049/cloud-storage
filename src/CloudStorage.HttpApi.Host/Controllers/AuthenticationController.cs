using CloudStorage.Application.Contracts.Users;
using CloudStorage.Application.Contracts.Users.Views;
using Microsoft.AspNetCore.Mvc;

namespace token.Controllers;

/// <summary>
/// 授权认证
/// </summary>
[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    /// <inheritdoc />
    public AuthenticationController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<string> LoginAsync(CreateTokenInput input)
    {
        var token = await _userInfoService.CreateTokenAsync(input);

        return token;
    }
    
}