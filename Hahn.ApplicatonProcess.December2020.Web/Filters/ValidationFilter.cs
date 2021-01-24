using Hahn.ApplicatonProcess.December2020.Web.Localize;
using Hahn.ApplicatonProcess.December2020.Web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(IStringLocalizer<Resource> localizer, ILogger<ValidationFilter> logger)
        {
            _logger = logger;
            _localizer = localizer;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }
            else
            {
                var data = from kvp in context.ModelState
                           from err in kvp.Value.Errors
                           let k = kvp.Key
                           select new ValidationError(k, string.IsNullOrEmpty(err.ErrorMessage) ? _localizer["Invalid Input"] : _localizer[err.ErrorMessage]);

                var response = new BadRequestObjectResult(new Result(data, (int)HttpStatusCode.BadRequest, _localizer["Entity is not valid"]));
                _logger.LogError(context.ActionDescriptor.DisplayName + response.ToString());
                context.Result = response;
            }

        }
    }
}
