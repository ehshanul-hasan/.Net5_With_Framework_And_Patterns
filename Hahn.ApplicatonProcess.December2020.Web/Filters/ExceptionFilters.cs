using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Web.Localize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Filters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IUnitOfWork unitOfWork, IStringLocalizer<Resource> localizer, ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            _unitOfWork.RollBackAsync();
            ObjectResult result;
            result = new ObjectResult(new
            {
                Status = 500,
                Message = _localizer["Something went wrong"].Value,
                Errors = CollectErrors(context)
            })
            {
                StatusCode = 500
            };
            context.Result = result;
            return Task.CompletedTask;
        }

        private List<string> CollectErrors(ExceptionContext context)
        {
            List<string> errors = new List<string>();
            Exception ex = context.Exception;
            while (ex != null)
            {
                _logger.LogError(ex.ToString());
                errors.Add(ex.Message);
                ex = ex.InnerException;
            }
            return errors;
        }
    }
}
