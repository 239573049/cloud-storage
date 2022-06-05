using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using token.HttpApi.Module;

namespace token.HttpApi.filters;

/// <summary>
/// 全局返回拦截
/// </summary>
public class GlobalResponseFilter : ActionFilterAttribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    [DebuggerStepThrough]
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result != null)
        {
            if (context.Result is ObjectResult)
            {
                ObjectResult objectResult = context.Result as ObjectResult;
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
                        Code = modelStateResult?.Code.ToString(),
                        Data = new
                        {

                        },
                        Message = modelStateResult?.Message
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
                ModelStateResult modelStateResult2 = context.Result as ModelStateResult;
                context.Result = new JsonResult(new
                {
                    Code=modelStateResult2?.Code.ToString(),
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