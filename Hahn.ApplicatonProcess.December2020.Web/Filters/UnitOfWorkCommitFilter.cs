using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Filters
{
    public class UnitOfWorkCommitFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();

            if (result.Exception == null && context.HttpContext.Request.Method != "GET")
            {
                var unitOfWork = result.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                await unitOfWork.CommitAsync();
            }
        }
    }
}
