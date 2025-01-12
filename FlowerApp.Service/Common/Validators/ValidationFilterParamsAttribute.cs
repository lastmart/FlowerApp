﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlowerApp.Service.Common.Validators;

public class ValidationFilterParamsAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.ActionArguments.ContainsKey("name"))
        {
            string name = filterContext.ActionArguments["name"] as string;

            if (name != null && name.Length > 10)
            {
                filterContext.Result = new BadRequestObjectResult("The length of name must not exceed 10");
            }
        }
    }
}