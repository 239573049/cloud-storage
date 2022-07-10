using System.Diagnostics;
using CloudStorage.HttpApi.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudStorage.HttpApi.filters;

public class GlobalModelStateValidationFilter : ActionFilterAttribute
{
    [DebuggerStepThrough]
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        ModelStateResult modelStateResult = new();
        foreach (var value in context.ModelState.Values)
        foreach (var error in value.Errors)
            modelStateResult.Message = modelStateResult.Message + error.ErrorMessage + "|";
        context.Result = new ObjectResult(modelStateResult);
    }
}