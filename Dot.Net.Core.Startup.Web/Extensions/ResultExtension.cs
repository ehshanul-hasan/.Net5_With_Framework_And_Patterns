using Dot.Net.Core.Startup.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Web.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToResult<T>(this T model, int status = 200, string message = default)
        {
            return new Result(model, status, message);
        }

        public static ActionResult ToOkResult<T>(this T model, string message = default, int status = 200)
        {
            return new OkObjectResult(model.ToResult(status, message));
        }

        public static ActionResult ToCreatedResult<T>(this T value, string location = "", string message = default, int status = 201)
        {
            return new CreatedResult(location, value.ToResult(status, message));
        }

        public static ActionResult ToCreatedAtActionResult<T>(this T value, string actionName, string controllerName, object routeValues, string message = default, int status = 201)
        {
            return new CreatedAtActionResult( actionName, controllerName,  routeValues, value.ToResult(status, message));
        }
    }
}
