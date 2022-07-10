using System.Diagnostics;
using CloudStorage.HttpApi.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudStorage.HttpApi.filters;

/// <summary>
///     全局返回拦截
/// </summary>
public class GlobalResponseFilter : ActionFilterAttribute
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    [DebuggerStepThrough]
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result != null)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult?.GetType().Name == "BadRequestObjectResult")
                {
                    context.Result = new JsonResult(new
                    {
                        Code = objectResult.StatusCode.ToString(),
                        Data = new
                        {
                        },
                        Message = objectResult.Value
                    });
                }
                else if (objectResult?.Value?.GetType().Name == "ModelStateResult")
                {
                    var modelStateResult = objectResult.Value as ModelStateResult;
                    context.Result = new JsonResult(new
                    {
                        modelStateResult?.Code,
                        Data = new
                        {
                        },
                        modelStateResult?.Message
                    });
                }
                else
                {
                    context.Result = new JsonResult(new
                    {
                        Code = 200.ToString(), Data = objectResult?.Value
                    });
                }
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new JsonResult(new
                {
                    Code = 200.ToString(),
                    Data = new
                    {
                    }
                });
            }
            else if (context.Result is ModelStateResult)
            {
                var modelStateResult2 = context.Result as ModelStateResult;
                context.Result = new JsonResult(new
                {
                    modelStateResult2?.Code,
                    Data = new
                    {
                    },
                    modelStateResult2?.Message
                });
            }
        }

        base.OnActionExecuted(context);
    }
}