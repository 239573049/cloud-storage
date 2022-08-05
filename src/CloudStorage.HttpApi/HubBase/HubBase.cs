using CloudStorage.Domain.Shared;
using CloudStorage.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace CloudStorage.HttpApi.HubBase;

public class HubBase : Hub
{
    public string GetUserId()
    {
        return Context.User?.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == Constants.Id)?.Value ??
               throw new Exception("未登录");
    }

    private UserInfo? GetUser()
    {
        var json = Context.User?.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == Constants.User)
                       ?.Value ??
                   throw new Exception("未登录");

        return JsonConvert.DeserializeObject<UserInfo>(json);
    }

    [HubMethodName("error")]
    public Task ErrorAsync(string message, int code = 400)
    {
        _ = Clients.Client(Context.ConnectionId).SendAsync("error", message, code);
        
        return Task.CompletedTask;
    }
}