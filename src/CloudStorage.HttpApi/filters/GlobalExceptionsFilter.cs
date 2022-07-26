using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Volo.Abp;

namespace CloudStorage.HttpApi.filters;

/// <summary>
///     全局异常拦截器
/// </summary>
public class GlobalExceptionsFilter : ExceptionFilterAttribute
{
    private const string Message = "GoYes";
    private readonly ILogger<GlobalExceptionsFilter> _loggerHelper;

    /// <summary>
    /// </summary>
    /// <param name="loggerHelper"></param>
    public GlobalExceptionsFilter(ILogger<GlobalExceptionsFilter> loggerHelper)
    {
        _loggerHelper = loggerHelper;
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    [DebuggerStepThrough]
    public override void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled == false)
        {
            var ex = context.Exception as BusinessException;
            var response = new
            {
                code = ex?.Code, message = ex?.Message ?? context.Exception.Message
            };

            _loggerHelper.LogError("{0}:  错误信息：{1}  错误path：{2}", DateTime.Now, response.message,
                context.HttpContext.Request.Path);

            context.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(response), 
                StatusCode = 200,
                ContentType = "application/json;charset=utf-8"
            };
        }

        context.ExceptionHandled = true;
    }
}