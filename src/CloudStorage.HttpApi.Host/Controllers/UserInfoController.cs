using CloudStorage.Application.Contracts.Users;
using CloudStorage.Application.Contracts.Users.Views;
using Microsoft.AspNetCore.Mvc;

namespace token.Controllers;

[ApiController]
[Route("api/userinfo")]
public class UserInfoController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }
    
    [HttpPost("userinfo")]
    public async Task CreateUserInfoAsync(UserInfoDto dto)
    {
        await _userInfoService.CreateUserInfoAsync(dto);
    }
}