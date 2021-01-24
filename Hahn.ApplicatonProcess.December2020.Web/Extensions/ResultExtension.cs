using Hahn.ApplicatonProcess.December2020.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToResult<T>(this T model, int status = 200, string message = default)
        {
            return new Result(model, status, message);
        }

        public static ActionResult ToOkResult<T>(this T model, int status = 200, string message = default)
        {
            return new OkObjectResult(model.ToResult(status, message));
        }

        public static ActionResult ToCreatedResult<T>(this T value, string location = "", int status = 201, string message = default)
        {
            return new CreatedResult(location, value.ToResult(status, message));
        }
    }
}
