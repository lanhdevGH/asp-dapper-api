using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WebApiDapper.ExceptionFilters.ExceptionModels;

namespace WebApiDapper.ExceptionFilters
{
    public class ExceptionHandleFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var errorDetail = new ExceptionModel
            {
                Message = context.Exception.Message,
                StatusCode = StatusCodes.Status501NotImplemented,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(errorDetail) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }
    }
}
